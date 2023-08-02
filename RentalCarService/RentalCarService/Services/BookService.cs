using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace RentalCarService.Services
{
    public class BookService : IBookService
    {
        private readonly RentalCarsDBContext _dbContext;

        public BookService(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertNewBook(Book book)
        {
            book.Category = FindCategoryDB(book);
            book.User = FindCountryDB(book.User.Id);
            book.Code = GenerateBookNumber();
            book.ValueToPay = CalculateValueToPay(book);

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        private string GenerateBookNumber()
        {
            int size = 6;
            Random random = new Random();
            string AlphabetNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = "";


            char[] chars = new char[size];
            for (int Position = 0; Position < size; Position++)
            {
                chars[Position] = AlphabetNumbers[random.Next(AlphabetNumbers.Length)];
                code= code + chars[Position];
            }

            CheckBookNumberCodeDB(code);
            return code;
        }

        private void CheckBookNumberCodeDB(string Code)
        {
            var Book = _dbContext.Books.Where(c => c.Code == Code).FirstOrDefault();

            if (Book != null)
            {
                GenerateBookNumber();
            }
        }

        private double CalculateValueToPay(Book Book)
        {
            double TotalValueToPay = ValueToPayPerDay(Book) + CalculateExtraCosts(Book);

            return TotalValueToPay;
        }

        private double ValueToPayPerDay(Book Book)
        {
            Categories Category = FindCategoryDB(Book);

            int DaysTotal = Book.ReturnDay.Day - Book.StartDay.Day;
            double PriceDay = 0;


            foreach(PriceBands p in Category.PriceBands)
            {
                if(DaysTotal >= p.MinDays && DaysTotal <= p.MaxDays)
                {
                    PriceDay = p.Price;
                    break;
                }
            }

            double PriceTotalDays = PriceDay * DaysTotal;
            return PriceTotalDays;
        }

        private double CalculateExtraCosts(Book Book)
        {
            int DaysTotal = Book.ReturnDay.Day - Book.StartDay.Day;

            double ExtraCosts = 0;
            foreach (BookExtra b in Book.BookExtra)
            {
                Extraa extra = FindExtraCost(b.Extra.Id);

                ExtraCosts = ExtraCosts + extra.DayCost * DaysTotal;
                ExtraCosts += extra.FixedCost;
            }
            return ExtraCosts;
        }

        private Extraa FindExtraCost(int Id)
        {
            Extraa extra = _dbContext.Extras.Find(Id);
            return extra;

        }
        private Categories FindCategoryDB(Book Book)
        {
            Categories Category = _dbContext.Categories.Include(P => P.PriceBands).Where(C => C.Id == Book.Category.Id).FirstOrDefault();
            return Category;
        }

        private User FindCountryDB(int Id)
        {
            User User = _dbContext.Users
                .Include(c => c.CountryCNH)
                .Include(n => n.Nationality)
                .Include(a => a.Address)
                .ThenInclude(ac => ac.Country)
                .Where(I => I.Id == Id)
                .FirstOrDefault();

            return User;
        }


    }
}



