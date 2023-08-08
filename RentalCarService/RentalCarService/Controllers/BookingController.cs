using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;

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

        public void InsertNewBook(Booking book)
        {
            BookService.InsertNewBook(book);
        }
    }
}
