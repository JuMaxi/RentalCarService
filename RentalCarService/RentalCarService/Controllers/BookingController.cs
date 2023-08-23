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
            BookService = bookService;
        }

        [HttpPost]
        public void InsertNewBook(BookingRequest bookingRequest)
        {
            Booking booking = ConvertToBooking(bookingRequest);
            BookService.InsertNewBook(booking);
        }

        [HttpGet("availability")]
        public List<AvailabilityResponse> ReturnAvailabilityCategories([FromQuery] AvailabilityRequest availability)
        {
            List<AvailabilityResponse> availabilityCategories = BookService.ReturnAvailabilityCategories(availability);

            return availabilityCategories;
        }

        [HttpGet]
        public List<BookingResponse> ReadBookingsFromDB()
        {
            List<Booking> bookings = BookService.ReadBookingsFromDB();

            List<BookingResponse> bookingsResponse = ConvertToBookingResponse(bookings);

            return bookingsResponse;
        }

        private List<BookingResponse> ConvertToBookingResponse(List<Booking> bookings)
        {
            List<BookingResponse> bookingsResponse = new List<BookingResponse>();
            foreach (Booking booking in bookings)
            {
                BookingResponse b = new BookingResponse();

                b.Code = booking.Code;
                b.User = booking.User.Name;
                b.Category = booking.Category.Description;
                b.BranchGet = booking.BranchGet.Name;
                b.BranchReturn = booking.BranchReturn.Name;

                List<ExtraResponse> extrasResponse = new List<ExtraResponse>();

                foreach (BookingExtra be in booking.BookExtra)
                {
                    ExtraResponse extraR = new ExtraResponse();

                    extraR.Service = be.Extra.Service;
                    extraR.DayCost = be.Extra.DayCost;
                    extraR.FixedCost = be.Extra.FixedCost;

                    extrasResponse.Add(extraR);
                }

                b.Extras = extrasResponse;
                b.Total = booking.ValueToPay;
                b.Start = booking.StartDay;
                b.Return = booking.ReturnDay;

                bookingsResponse.Add(b);
            }

            return bookingsResponse;
        }
        private Booking ConvertToBooking(BookingRequest bookingRequest)
        {
            Booking booking = new Booking();

            User user = new User();
            user.Id = bookingRequest.UserId;
            booking.User = user;

            Categories category = new Categories();
            category.Id = bookingRequest.CategoryId;
            booking.Category = category;

            Branchs branch = new Branchs();
            branch.Id = bookingRequest.BranchGetId;
            booking.BranchGet = branch;
            branch.Id = bookingRequest.BranchReturnId;
            booking.BranchReturn = branch;

            List<BookingExtra> extras = new List<BookingExtra>();

            foreach (int id in bookingRequest.Extras)
            {
                BookingExtra bookingExtra = new BookingExtra();

                Extraa extra = new Extraa();
                extra.Id = id;

                bookingExtra.Extra = extra;

                extras.Add(bookingExtra);
            }

            booking.BookExtra = extras;

            booking.StartDay = bookingRequest.Start;
            booking.ReturnDay = bookingRequest.Return;

            return booking;
        }
    }
}
