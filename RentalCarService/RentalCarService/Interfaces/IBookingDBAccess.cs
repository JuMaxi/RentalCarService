using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IBookingDBAccess
    {
        public List<Booking> GetListBookingByCategoryIdAndDatesStartReturn(Booking booking);
        public void AddNewBook(Booking booking);
        public List<Booking> GetBookingThenInclude();
        public Booking GetBookingByCode(string code);
    }
}
