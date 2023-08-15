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
    public class BookingService : IBookingService
    {
        private readonly RentalCarsDBContext _dbContext;
        IValidateBooking _validateBook;

        public BookingService(RentalCarsDBContext dbContext, IValidateBooking validateBook)
        {
            _dbContext = dbContext;
            _validateBook = validateBook;
        }

        public void InsertNewBook(Booking booking)
        {
            _validateBook.Validate(booking);

            booking.Category = FindCategoryDB(booking);
            booking.BranchGet = FindBranchDB(booking.BranchGet.Id);
            booking.BranchReturn = FindBranchDB(booking.BranchReturn.Id);
            booking.User = FindCountryDB(booking.User.Id);
            booking.Code = GenerateBookNumber();
            booking.BookExtra = SaveExtraToBookExtra(booking);
            booking.ValueToPay = CalculateValueToPay(booking);

            _dbContext.Books.Add(booking);
            _dbContext.SaveChanges();
        }

        private List<BookingExtra> SaveExtraToBookExtra(Booking booking)
        {
            List<Extraa> listExtra = FindExtraDB(booking);
            for (int i = 0; i < listExtra.Count; i++)
            {
                booking.BookExtra[i].Extra = listExtra[i];
            }
            return booking.BookExtra;
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

        private double CalculateValueToPay(Booking booking)
        {
            double TotalValueToPay = ValueToPayPerDay(booking) + CalculateExtraCosts(booking);

            return TotalValueToPay;
        }

        private double ValueToPayPerDay(Booking booking)
        {
            Categories category = FindCategoryDB(booking);

            int daysTotal = CalculateDaysBook(booking);

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

        private double CalculateExtraCosts(Booking booking)
        {
            int DaysTotal = CalculateDaysBook(booking);

            double ExtraCosts = 0;

            List<Extraa> listExtra = FindExtraDB(booking);

            foreach (Extraa b in listExtra)
            {
                ExtraCosts = ExtraCosts + b.DayCost * DaysTotal;
                ExtraCosts += b.FixedCost;
            }
            return ExtraCosts;
        }

        private List<Extraa> FindExtraDB(Booking booking)
        {
            var ids = booking.BookExtra.Select(bookExtra => bookExtra.Extra.Id).ToList(); // [1, 4]
            var listExtras = _dbContext.Extras.Where(extra => ids.Contains(extra.Id)).ToList(); // usa a lista para filtro
            return listExtras;

        }

        private Branchs FindBranchDB(int id)
        {
            var branch = _dbContext.Branches.Include(O => O.OpeningHours).Where(B => B.Id == id).FirstOrDefault();
            return branch;
        }
        private Categories FindCategoryDB(Booking booking)
        {
            Categories Category = _dbContext.Categories.Include(P => P.PriceBands).Where(C => C.Id == booking.Category.Id).FirstOrDefault();
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

        private int CalculateDaysBook(Booking booking)
        {
            if (booking.HourReturnCar <= booking.HourGetCar)
            {
                return (booking.ReturnDay.Day - booking.StartDay.Day);
            }
            else
            {
                return (booking.ReturnDay.Day - booking.StartDay.Day) + 1;
            }
        }


    }
}



