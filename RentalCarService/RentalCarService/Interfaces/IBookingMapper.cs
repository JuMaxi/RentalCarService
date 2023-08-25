using RentalCarService.Models.Responses;
using RentalCarService.Models;
using System.Collections.Generic;
using RentalCarService.Models.Requests;

namespace RentalCarService.Interfaces
{
    public interface IBookingMapper
    {
        public List<BookingResponse> ConvertToBookingResponse(List<Booking> bookings);
        public Booking ConvertToBooking(BookingRequest bookingRequest);
    }
}
