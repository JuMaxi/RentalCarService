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

       
    }
}
