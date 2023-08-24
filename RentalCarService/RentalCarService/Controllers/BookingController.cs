using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookingController : ControllerBase
    {
        readonly IBookingService _bookService;
        readonly IBookingMapper _bookingMapper;

        public BookingController(IBookingService bookService, IBookingMapper bookingMapper)
        {
            _bookService = bookService;
            _bookingMapper = bookingMapper;
        }

        [HttpPost]
        public void InsertNewBook(BookingRequest bookingRequest)
        {
            Booking booking = _bookingMapper.ConvertToBooking(bookingRequest);
            _bookService.InsertNewBook(booking);
        }

        [HttpGet("availability")]
        public List<AvailabilityResponse> ReturnAvailabilityCategories([FromQuery] AvailabilityRequest availability)
        {
            List<AvailabilityResponse> availabilityCategories = _bookService.ReturnAvailabilityCategories(availability);

            return availabilityCategories;
        }

        [HttpGet]
        public List<BookingResponse> ReadBookingsFromDB()
        {
            List<Booking> bookings = _bookService.ReadBookingsFromDB();

            List<BookingResponse> bookingsResponse = _bookingMapper.ConvertToBookingResponse(bookings);

            return bookingsResponse;
        }

        
    }
}
