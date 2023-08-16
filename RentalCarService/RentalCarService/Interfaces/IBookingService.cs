using RentalCarService.Models;
using RentalCarService.Models.Responses;

namespace RentalCarService.Interfaces
{
    public interface IBookingService
    {
        public void InsertNewBook(Booking book);
        public void Testing(Availability availability);
    }
}
