using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Validators;

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

        [Fact]
        public void When_branchget_hour_is_not_open_should_throw_new_exception()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04).DayOfWeek,
                            Opens = new TimeOnly(09, 00, 00)
                        }
                    }
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
                .WithMessage("This branch opens at " + newBooking.BranchGet.OpeningHours[0].Opens + " you can't get the car before this time.");
        }

        [Fact]
        public void When_branchget_dayofweek_is_not_open_should_throw_new_exception()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 10, 04).DayOfWeek,
                            Opens = new TimeOnly(09, 00, 00)
                        }
                    }
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

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchesFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("This branch is not open on day " + newBooking.StartDay.DayOfWeek + ". Please select a day that this branch is open.");
        }

        [Fact]
        public void When_branchreturn_is_zero_should_throw_new_exception()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04).DayOfWeek,
                            Opens = new TimeOnly(07, 00, 00),
                        }
                    }
                },
                BranchReturn= new()
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

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(newBooking.BranchReturn.Id).Returns(newBooking.BranchReturn);
            dbAccessBranchesFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchesFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("The Branch to return the car must be chosen to continue.");
        }

        [Fact]
        public void When_branchreturn_hour_is_not_open_should_throw_new_exception()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04).DayOfWeek,
                            Opens = new TimeOnly(07, 00, 00),
                        }
                    }
                },
                BranchReturn = new()
                {
                    Id = 2,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 05, 17, 00, 00).DayOfWeek,
                            Closes = new TimeOnly(16, 00, 00)
                        }
                    }
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            Car car = new();
            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCarByCategoryId(Arg.Any<int>()).Returns(car);

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);
            dbAccessBranchesFake.GetBranchById(newBooking.BranchReturn.Id).Returns(newBooking.BranchReturn);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchesFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("This branch closes at " + newBooking.BranchReturn.OpeningHours[0].Closes + " you can't return the car after this time.");
        }

        [Fact]
        public void When_branchreturn_dayofweek_is_not_open_should_throw_new_exception()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04).DayOfWeek,
                            Opens = new TimeOnly(07, 00, 00),
                        }
                    }
                },
                BranchReturn = new()
                {
                    Id = 2,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04, 17, 00, 00).DayOfWeek,
                            Closes = new TimeOnly(18, 00, 00)
                        }
                    }
                }
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            Car car = new();
            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCarByCategoryId(newBooking.Category.Id).Returns(car);

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);
            dbAccessBranchesFake.GetBranchById(newBooking.BranchReturn.Id).Returns(newBooking.BranchReturn);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchesFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().Throw<Exception>()
                .WithMessage("This branch is not open on day " + newBooking.ReturnDay.DayOfWeek + ". Please select a day that this branch is open.");
        }

        [Fact]
        public void When_the_validator_booking_doesnot_have_any_exceptions()
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
                    Id = 1,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 04).DayOfWeek,
                            Opens = new TimeOnly(07, 00, 00),
                        }
                    }
                },
                BranchReturn = new()
                {
                    Id = 2,
                    OpeningHours = new()
                    {
                        new()
                        {
                            DayOfWeek = new DateTime(2023, 09, 05, 17, 00, 00).DayOfWeek,
                            Closes = new TimeOnly(18, 00, 00)
                        }
                    }
                },
            };

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.GetListBookingByCategoryIdAndDatesStartReturn(newBooking).ReturnsNull();

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.ExistsAvailabilityForBooking(Arg.Any<Booking>(), Arg.Any<List<Booking>>(), Arg.Any<int>()).Returns(true);

            Car car = new();
            var dbAccessCarFake = Substitute.For<ICarDBAccess>();
            dbAccessCarFake.GetCarByCategoryId(newBooking.Category.Id).Returns(car);

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(newBooking.BranchGet.Id).Returns(newBooking.BranchGet);
            dbAccessBranchesFake.GetBranchById(newBooking.BranchReturn.Id).Returns(newBooking.BranchReturn);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserById(newBooking.User.Id).Returns(newBooking.User);

            ValidateBooking validatorBooking = new(dbAccessBookingFake, availabilityService, dbAccessCarFake, dbAccessBranchesFake, dbAccessUserFake);

            validatorBooking.Invoking(validator => validator.Validate(newBooking))
                .Should().NotThrow<Exception>();
        }
    }
}
