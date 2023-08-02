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
            book.BranchGet = FindBranchDB(book.BranchGet.Id);
            book.BranchReturn = FindBranchDB(book.BranchReturn.Id);
            book.User = FindCountryDB(book.User.Id);
            book.Code = GenerateBookNumber();
            book.BookExtra = SaveExtraToBookExtra(book);
            book.ValueToPay = CalculateValueToPay(book);

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        private List<BookExtra> SaveExtraToBookExtra(Book book)
        {
            List<Extraa> listExtra = FindExtraDB(book);
            for (int i = 0; i < listExtra.Count; i++)
            {
                book.BookExtra[i].Extra = listExtra[i];
            }
            return book.BookExtra;
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

            int DaysTotal = (Book.ReturnDay.Day - Book.StartDay.Day) + 1;
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
            int DaysTotal = (Book.ReturnDay.Day - Book.StartDay.Day) + 1;

            double ExtraCosts = 0;

            List<Extraa> listExtra = FindExtraDB(Book);

            foreach (Extraa b in listExtra)
            {
                ExtraCosts = ExtraCosts + b.DayCost * DaysTotal;
                ExtraCosts += b.FixedCost;
            }
            return ExtraCosts;
        }

        private List<Extraa> FindExtraDB(Book Book)
        {
            var ids = Book.BookExtra.Select(bookExtra => bookExtra.Extra.Id).ToList(); // [1, 4]
            var listExtras = _dbContext.Extras.Where(extra => ids.Contains(extra.Id)).ToList(); // usa a lista para filtro
            return listExtras;

        }

        private Branchs FindBranchDB(int id)
        {
            var branch = _dbContext.Branches.Include(O => O.OpeningHours).Where(B => B.Id == id).FirstOrDefault();
            return branch;
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



