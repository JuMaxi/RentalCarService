using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IAvailabilityService
    {
        bool ExistsAvailabilityForBooking(Book candidate, List<Book> nearbyBookings);
    }
}
