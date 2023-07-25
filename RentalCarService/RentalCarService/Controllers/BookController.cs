using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BookController : ControllerBase
    {
        IBookService BookService;

        public BookController(IBookService bookService)
        {
            BookService= bookService;
        }
    }
}
