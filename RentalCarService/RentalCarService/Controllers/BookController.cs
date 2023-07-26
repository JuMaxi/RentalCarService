using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;

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

        public void InsertNewBook(Book book)
        {
            BookService.InsertNewBook(book);
        }
    }
}
