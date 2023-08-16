using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Interfaces.Responses;
using RentalCarService.Models;
using RentalCarService.Models.Responses;

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
        public void CheckAvailability([FromQuery] Availability availability)
        {
            BookService.Testing(availability);
        }
    }
}
