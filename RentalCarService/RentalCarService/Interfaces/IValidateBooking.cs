using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IValidateBooking
    {
        public void Validate(Booking book);
    }
}
