using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IAvailabilityService
    {
        bool ExistsAvailabilityForBooking(Booking candidate, List<Booking> nearbyBookings, int amountCarsInCategory = 1);
    }
}
