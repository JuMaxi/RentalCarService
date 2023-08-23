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
        readonly IBookingService BookService;

        public BookingController(IBookingService bookService)
        {
            BookService= bookService;
        }

        [HttpPost]
        public void InsertNewBook(BookingRequest bookingRequest)
        {
            Booking booking= ConvertToBooking(bookingRequest);
            BookService.InsertNewBook(booking);
        }

        [HttpGet]
        public List<AvailabilityResponse> ReturnAvailabilityCategories([FromQuery] AvailabilityRequest availability)
        {
            List<AvailabilityResponse> availabilityCategories =  BookService.ReturnAvailabilityCategories(availability);

            return availabilityCategories;
        }

        private Booking ConvertToBooking(BookingRequest bookingRequest)
        {
            Booking booking = new Booking();

            User user= new User();
            user.Id= bookingRequest.UserId;
            booking.User= user;

            Categories category= new Categories();
            category.Id= bookingRequest.CategoryId;
            booking.Category= category;

            Branchs branch= new Branchs();
            branch.Id= bookingRequest.BranchGetId;
            booking.BranchGet= branch;
            branch.Id= bookingRequest.BranchReturnId;
            booking.BranchReturn= branch;

            List<BookingExtra> extras = new List<BookingExtra>();

            foreach(int id in bookingRequest.Extras)
            {
                BookingExtra bookingExtra = new BookingExtra();

                Extraa extra = new Extraa();
                extra.Id = id;

                bookingExtra.Extra = extra;

                extras.Add(bookingExtra);
            }

            booking.BookExtra= extras;

            booking.StartDay = bookingRequest.Start;
            booking.ReturnDay= bookingRequest.Return;

            return booking;
        }
    }
}
