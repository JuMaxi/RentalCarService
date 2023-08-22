using RentalCarService.Models;
using RentalCarService.Models.Requests;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IAvailabilityService
    {
        bool ExistsAvailabilityForBooking(Booking candidate, List<Booking> nearbyBookings, int amountCarsInCategory = 1);
        public List<Categories> SaveListAvailableCategories(AvailabilityRequest availability);
    }
}
