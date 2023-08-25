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
            BookingRequest bookingRequest = new()
            {
                UserId = 2,
                CategoryId = 3,
                BranchGetId = 4,
                BranchReturnId = 5,
                Start = new DateTime(2023, 08, 25),
                Return = new DateTime(2023, 08, 27)
            };
            bookingRequest.Extras.Add(6);
            bookingRequest.Extras.Add(7);

            BookingMapper bookingMapper = new();

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
            List<Booking> bookings = new();

            Booking booking = new ()
            {
                Code = "KIJG48",
                User = new() { Name = "Joao da Silva" },
                Category = new() { Description = "Economy" },
                BranchGet = new() { Name = "Happy Cars Ltda" },
                BranchReturn = new() { Name = "Peace Cars Ltda" },
                ValueToPay = 355,
                StartDay = new DateTime(2023, 08, 25),
                ReturnDay = new DateTime(2023, 08, 27)
            };

            Extraa extra = new() { Service = "GPS", DayCost = 5, FixedCost = 30 };

            BookingExtra bookingExtra = new() { Extra = extra };

            List<BookingExtra> bookingsExtras = new() { bookingExtra };
            booking.BookExtra = bookingsExtras;

            bookings.Add(booking);

            BookingMapper bookingMapper = new();

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
