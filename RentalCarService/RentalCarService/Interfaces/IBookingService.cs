using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IBookingService
    {
        public void InsertNewBook(Booking book);
        public List<AvailabilityResponse> ReturnAvailabilityCategories(AvailabilityRequest availability);
    }
}
