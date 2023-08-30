using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IBookingDBAccess
    {
        public List<Booking> GetListBookingByCategoryIdAndDatesStartReturn(Booking booking);
    }
}
