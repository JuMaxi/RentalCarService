using FluentAssertions;
using RentalCarService.Mappers;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;


namespace RentarlCars.Tests.Mappers
{
    public class BookingMapperTests
    {
        [Fact]
        public void Checking_if_BookingRequest_is_Equal_Booking()
        {
            BookingRequest bookingRequest = new BookingRequest();
            bookingRequest.UserId = 2;
            bookingRequest.CategoryId = 3;
            bookingRequest.BranchGetId = 4;
            bookingRequest.BranchReturnId = 5;
            bookingRequest.Extras.Add(6);
            bookingRequest.Extras.Add(7);
            bookingRequest.Start = new DateTime(2023, 08, 25);
            bookingRequest.Return = new DateTime(2023, 08, 27);

            BookingMapper bookingMapper = new BookingMapper();

            Booking booking = bookingMapper.ConvertToBooking(bookingRequest);

            booking.User.Id.Should().Be(bookingRequest.UserId);
            booking.Category.Id.Should().Be(bookingRequest.CategoryId);
            booking.BranchGet.Id.Should().Be(bookingRequest.BranchGetId);
            booking.BranchReturn.Id.Should().Be(bookingRequest.BranchReturnId);
            booking.BookExtra[0].Extra.Id.Should().Be(bookingRequest.Extras[0]);
            booking.BookExtra[1].Extra.Id.Should().Be(bookingRequest.Extras[1]);
            booking.StartDay.Should().Be(bookingRequest.Start);
            booking.ReturnDay.Should().Be(bookingRequest.Return);
        }

        [Fact]
        public void Checking_if_BookingResponse_is_Equal_Booking()
        {
            List<Booking> bookings = new List<Booking>();

            Booking booking = new Booking();

            booking.Code = "KIJG48";

            User user = new User();
            user.Name = "Joao da Silva";
            booking.User = user;

            Categories category= new Categories();
            category.Description= "Economy";
            booking.Category= category;

            Branchs branch= new Branchs();
            branch.Name= "Happy Cars Ltda";
            booking.BranchGet= branch;

            branch = new Branchs();
            branch.Name= "Peace Cars Ltda";
            booking.BranchReturn= branch;

            BookingExtra bookingExtra= new BookingExtra();
            Extraa extra = new Extraa();
            extra.Service= "GPS";
            extra.DayCost = 5;
            extra.FixedCost = 30;
            bookingExtra.Extra= extra;

            List<BookingExtra> bookingsExtras = new List<BookingExtra>();
            bookingsExtras.Add(bookingExtra);
            booking.BookExtra = bookingsExtras;

            booking.ValueToPay = 355;
            booking.StartDay = new DateTime(2023, 08, 25);
            booking.ReturnDay = new DateTime(2023, 08, 27);

            bookings.Add(booking);

            BookingMapper bookingMapper = new BookingMapper();

            List<BookingResponse> bookingsResponse = bookingMapper.ConvertToBookingResponse(bookings);

            bookingsResponse[0].Code.Should().Be(bookings[0].Code);
            bookingsResponse[0].User.Should().Be(bookings[0].User.Name);
            bookingsResponse[0].Category.Should().Be(bookings[0].Category.Description);
            bookingsResponse[0].BranchGet.Should().Be(bookings[0].BranchGet.Name);
            bookingsResponse[0].BranchReturn.Should().Be(bookings[0].BranchReturn.Name);
            bookingsResponse[0].Extras[0].Service.Should().Be(bookings[0].BookExtra[0].Extra.Service);
            bookingsResponse[0].Extras[0].DayCost.Should().Be(bookings[0].BookExtra[0].Extra.DayCost);
            bookingsResponse[0].Extras[0].FixedCost.Should().Be(bookings[0].BookExtra[0].Extra.FixedCost);
            bookingsResponse[0].Total.Should().Be(bookings[0].ValueToPay);
            bookingsResponse[0].Start.Should().Be(bookings[0].StartDay);
            bookingsResponse[0].Return.Should().Be(bookings[0].ReturnDay);
        }
    }
}
