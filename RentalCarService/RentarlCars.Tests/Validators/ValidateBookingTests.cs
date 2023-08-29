using FluentAssertions;
using NSubstitute;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests.Validators
{
    public class ValidateBookingTests
    {
        [Fact]
        public void When_there_is_no_availability_should_throw_new_exception()
        {
            List<Booking> nearbyBookings = new()
            {
                new()
                {
                    Category = new()
                    {
                        Id = 1004
                    },
                    StartDay = new DateTime(2023, 09, 01, 08, 00,00),
                    ReturnDay = new DateTime(2023, 09, 03, 17, 00, 00)
                },
                new()
                {
                    Category = new()
                    {
                        Id = 1004
                    },
                    StartDay = new DateTime(2023, 09, 06, 09, 00, 00),
                    ReturnDay = new DateTime(2023, 09, 06, 17, 00, 00)
                }
            };

            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 1004
                },
                StartDay = new DateTime(2023, 09, 01, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 06, 17, 00, 00)
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).Returns(nearbyBookings);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCountFleetByCategoryId(newBooking.Category.Id).Returns(2);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, null, dbAccessCarFake, null, null);

            
        }
    }
}
