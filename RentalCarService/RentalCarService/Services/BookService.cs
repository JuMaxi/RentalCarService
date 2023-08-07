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
        IValidateBook _validateBook;

        public BookService(RentalCarsDBContext dbContext, IValidateBook validateBook)
        {
            _dbContext = dbContext;
            _validateBook = validateBook;
        }

        public void InsertNewBook(Book book)
        {
            _validateBook.Validate(book);

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
                code = code + chars[Position];
            }

            return CheckBookNumberCodeDB(code);
        }

        private string CheckBookNumberCodeDB(string code)
        {
            var Book = _dbContext.Books.Where(c => c.Code == code).FirstOrDefault();

            if (Book != null)
            {
                code = GenerateBookNumber();
            }
            return code;
        }

        private double CalculateValueToPay(Book book)
        {
            double TotalValueToPay = ValueToPayPerDay(book) + CalculateExtraCosts(book);

            return TotalValueToPay;
        }

        private double ValueToPayPerDay(Book book)
        {
            Categories category = FindCategoryDB(book);

            int daysTotal = CalculateDaysBook(book);

            double PriceDay = 0;


            foreach (PriceBands p in category.PriceBands)
            {
                if (daysTotal >= p.MinDays && daysTotal <= p.MaxDays)
                {
                    PriceDay = p.Price;
                    break;
                }
            }

            double PriceTotalDays = PriceDay * daysTotal;
            return PriceTotalDays;
        }

        private double CalculateExtraCosts(Book book)
        {
            int DaysTotal = CalculateDaysBook(book);

            double ExtraCosts = 0;

            List<Extraa> listExtra = FindExtraDB(book);

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

        private int CalculateDaysBook(Book book)
        {
            if (book.HourReturnCar <= book.HourGetCar)
            {
                return (book.ReturnDay.Day - book.StartDay.Day);
            }
            else
            {
                return (book.ReturnDay.Day - book.StartDay.Day) + 1;
            }
        }

    }
}



