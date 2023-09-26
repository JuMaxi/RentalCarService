using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class BookingDBAccess : IBookingDBAccess
    {
        readonly RentalCarsDBContext _dbcontext;

        public BookingDBAccess(RentalCarsDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<Booking> GetListBookingByCategoryIdAndDatesStartReturn(Booking booking)
        {
            List<Booking> books = _dbcontext.Books
                .Include(c => c.Category)
                .Where(b => b.Category.Id == booking.Category.Id)
                .Where(d => d.StartDay <= booking.ReturnDay)
                .Where(c => c.StartDay >= booking.StartDay)
                .ToList();

            return books;
        }

        public void AddNewBook(Booking booking)
        {
            _dbcontext.Books.Add(booking);
            _dbcontext.SaveChanges();
        }
        public List<Booking> GetBookingThenInclude()
        {
            return _dbcontext.Books
                .Include(u => u.User)
                .Include(c => c.Category)
                .Include(b => b.BranchGet)
                .Include(b => b.BranchReturn)
                .Include(be => be.BookExtra)
                .ThenInclude(e => e.Extra)
                .ToList();
        }
        public Booking GetBookingByCode(string code)
        {
            return _dbcontext.Books.Where(c => c.Code == code).FirstOrDefault();
        }

    }
}
