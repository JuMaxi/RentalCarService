using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReturnsExtensions;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests.Validators
{
    public class ValidateBookingTests
    {
        [Fact]
        public void When_there_is_no_availability_should_throw_new_exception()
        {
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
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCountFleetByCategoryId(newBooking.Category.Id).Returns(2);

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(false);
            
            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, null, null);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("There is no availiability for the requested dates");
        }

        [Fact]
        public void When_user_id_is_zero_should_throw_new_exception()
        {
            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 1004,
                },
                StartDay = new DateTime(2023, 09, 04, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 05, 17, 00, 00),
                User = new()
                {
                    Id = 0
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCountFleetByCategoryId(newBooking.Category.Id).Returns(2);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, null, null);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("The User Id must be filled to continue.");
        }

        [Fact]
        public void When_user_id_is_not_valid_should_throw_new_exception()
        {
            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 1004,
                },
                StartDay = new DateTime(2023, 09, 04, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 05, 17, 00, 00),
                User = new()
                {
                    Id = 1
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCountFleetByCategoryId(newBooking.Category.Id).Returns(2);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).ReturnsNull();

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, null, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("You must fill the user with a valid Id.");
        }

        [Fact]
        public void When_category_id_is_zero_should_throw_new_exception()
        {
            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 0,
                },
                StartDay = new DateTime(2023, 09, 04, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 05, 17, 00, 00),
                User = new()
                {
                    Id = 1
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCountFleetByCategoryId(1004).Returns(2);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, null, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("The Category Id must be filled to continue.");
        }

        [Fact]
        public void When_category_car_from_db_is_null_should_throw_new_exception()
        {
            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 1004,
                },
                StartDay = new DateTime(2023, 09, 04, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 05, 17, 00, 00),
                User = new()
                {
                    Id = 1
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCarByCategoryId(newBooking.Category.Id).ReturnsNull();

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, null, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("There is no cars available in this category.");
        }

        [Fact]
        public void When_branchget_is_zero_should_throw_new_exception()
        {
            Booking newBooking = new()
            {
                Category = new()
                {
                    Id = 1004,
                },
                StartDay = new DateTime(2023, 09, 04, 08, 00, 00),
                ReturnDay = new DateTime(2023, 09, 05, 17, 00, 00),
                User = new()
                {
                    Id = 1
                },
                BranchGet = new()
                {
                    Id = 0
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            Car car = new();
            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCarByCategoryId(newBooking.Category.Id).Returns(car);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            var dbAccessBranchFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("The Branch to get the car must be chosen to continue.");
        }
    }
}
