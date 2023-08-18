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
        IBookingService BookService;

        public BookingController(IBookingService bookService)
        {
            BookService= bookService;
        }

        [HttpPost]
        public void InsertNewBook(Booking book)
        {
            BookService.InsertNewBook(book);
        }

        [HttpGet]
        public List<AvailabilityResponse> CheckAvailability([FromQuery] AvailabilityRequest availability)
        {
            BookService.Testing(availability);

            return null;
        }
    }
}
