using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using RentalCarService.Services;
using System.Collections.Specialized;

namespace RentarlCars.Tests.Services
{
    public class BookingServiceTests
    {
        [Fact]
        public void When_there_is_no_available_category_should_return_list_empty()
        {
            AvailabilityRequest availability = new();

            List<Categories> listCategories = new();
            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.SaveListAvailableCategories(availability).Returns(listCategories);

            var dbAccessBranchsFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchsFake.GetBranchById(0).ReturnsNull();

            BookingService bookingService = new(null, null, availabilityService, null, dbAccessBranchsFake, null, null);

            List<AvailabilityResponse> list = bookingService.ReturnAvailabilityCategories(availability);

            list.Count.Should().Be(0);
        }

        [Fact]
        public void When_there_is_three_categories_and_just_two_are_available_should_return_listcategories_count_equal_two()
        {
            AvailabilityRequest availability = new()
            {
                BranchGetCar = 1
            };
            List<Categories> listCategories = new()
            {
                new()
                {
                    Id = 1,
                },
                new()
                {
                    Id = 2,
                },
            };

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.SaveListAvailableCategories(availability).Returns(listCategories);

            Branchs branch = new();
            var dbAccessBranchFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchFake.GetBranchById(availability.BranchGetCar).Returns(branch);

            Categories category = new()
            {
                Id = 1,
                PriceBands = new()
                {
                    new()
                    {
                        Price = 10,
                    }
                },
            };
            var dbAccessCategoryFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessCategoryFake.GetCategoryById(listCategories[0].Id).Returns(category);
            dbAccessCategoryFake.GetCategoryById(listCategories[1].Id).Returns(category);


            BookingService bookingService = new(null, null, availabilityService, dbAccessCategoryFake, dbAccessBranchFake, null, null);

            var availabilityResponse = bookingService.ReturnAvailabilityCategories(availability);
            availabilityResponse.Count.Should().Be(2);
        }

        [Fact]
        public void When_there_is_a_category_available_should_return_right_estimative_to_pay()
        {
            AvailabilityRequest availability = new()
            {
                BranchGetCar = 1,
                StartDay = DateTime.Now,
                ReturnDay = DateTime.Now.AddDays(2)
            };
            List<Categories> listCategories = new()
            {
                new()
                {
                    Id = 1,
                    PriceBands = new()
                    {
                        new()
                        {
                            Price = 10,
                            MinDays = 1,
                            MaxDays = 2,
                        }
                    }
                }
            };

            var availabilityService = Substitute.For<IAvailabilityService>();
            availabilityService.SaveListAvailableCategories(availability).Returns(listCategories);

            Branchs branch = new();
            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(availability.BranchGetCar).Returns(branch);

            var dbAccessCategoriesFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessCategoriesFake.GetCategoryById(listCategories[0].Id).Returns(listCategories[0]);

            BookingService bookingService = new(null, null, availabilityService, dbAccessCategoriesFake, dbAccessBranchesFake, null, null);

            var availabilityResponse = bookingService.ReturnAvailabilityCategories(availability);
            availabilityResponse[0].Estimative.Should().Be(20);
        }

        [Fact]
        public void When_booking_is_not_valid_should_throw_new_exception()
        {
            Booking booking = new();

            var validateFake = Substitute.For<IValidateBooking>();
            validateFake.When(x => x.Validate(booking)).Do(x => { throw new Exception(); });

            BookingService bookingService = new(null, validateFake, null, null, null, null, null);

            bookingService.Invoking(b => b.InsertNewBook(booking))
                .Should().Throw<Exception>();
        }

        [Fact]
        public void When_booking_is_valid_should_not_throw_exception()
        {
            Booking booking = new()
            {
                Category = new()
                {
                    Id = 1004,
                    PriceBands = new()
                    {
                        new()
                        {
                            MinDays = 1,
                            MaxDays = 2,
                            Price = 10
                        }
                    }
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
                BookExtra = new()
                {
                    new()
                    {
                        Extra= new()
                        {
                            Id = 1,
                            DayCost = 5
                        }
                    }
                }
            };

            var validateFake = Substitute.For<IValidateBooking>();
            validateFake.Validate(booking);

            var dbAccessCategoriesFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessCategoriesFake.GetCategoryById(booking.Category.Id).Returns(booking.Category);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserByIdThenInclude(booking.User.Id).Returns(booking.User);

            var dbAccessBranchFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchFake.GetBranchById(booking.BranchGet.Id).Returns(booking.BranchGet);
            dbAccessBranchFake.GetBranchById(booking.BranchReturn.Id).Returns(booking.BranchReturn);

            List<Extraa> extras = new();
            extras.Add(booking.BookExtra[0].Extra);
            var dbAccessExtrasFake = Substitute.For<IExtraDBAccess>();
            dbAccessExtrasFake.GetExtraDB(booking).Returns(extras);

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.AddNewBook(booking);

            BookingService bookingService = new(dbAccessBookingFake, validateFake, null, dbAccessCategoriesFake, dbAccessBranchFake,
                dbAccessUserFake, dbAccessExtrasFake);

            bookingService.Invoking(b => b.InsertNewBook(booking))
                .Should().NotThrow<Exception>();
        }

        [Fact]
        public void Validating_code_booking_length_is_six_and_is_char_or_digit()
        {
            Booking booking = new()
            {
                Category = new()
                {
                    Id = 1,
                    PriceBands = new()
                    {
                        new()
                        {
                            MinDays = 1,
                            MaxDays = 2,
                            Price = 10
                        }
                    }
                },
                BranchGet = new()
                {
                    Id = 1,
                },
                BranchReturn = new()
                {
                    Id = 2
                },
                User = new()
                {
                    Id = 1
                },
                BookExtra = new()
                {
                    new()
                    {
                        Extra = new()
                        {
                            Id = 1,
                            DayCost = 5
                        }

                    }
                }
            };

            var validateFake = Substitute.For<IValidateBooking>();
            validateFake.Validate(booking);

            var dbAccessCategoriesFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessCategoriesFake.GetCategoryById(booking.Category.Id).Returns(booking.Category);

            var dbAccessBranchFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchFake.GetBranchById(booking.BranchGet.Id).Returns(booking.BranchGet);
            dbAccessBranchFake.GetBranchById(booking.BranchReturn.Id).Returns(booking.BranchReturn);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserByIdThenInclude(booking.User.Id).Returns(booking.User);

            List<Extraa> extras = new();
            extras.Add(booking.BookExtra[0].Extra);
            var dbAccessExtrasFake = Substitute.For<IExtraDBAccess>();
            dbAccessExtrasFake.GetExtraDB(booking).Returns(extras);

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.AddNewBook(booking);

            BookingService bookingService = new(dbAccessBookingFake, validateFake, null, dbAccessCategoriesFake,
                dbAccessBranchFake, dbAccessUserFake, dbAccessExtrasFake);

            bookingService.InsertNewBook(booking);

            dbAccessBookingFake.Received().AddNewBook(Arg.Is<Booking>(book => book.Code.Length == 6
            && CheckIfCharIsLetterOrDigit(book.Code) == true));
        }

        private bool CheckIfCharIsLetterOrDigit(string code)
        {
            foreach (char c in code)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        [Fact]
        public void When_hour_return_is_greater_than_hour_start_should_add_one_extra_day_to_pay()
        {
            Booking booking = new()
            {
                StartDay = DateTime.Now,
                ReturnDay = DateTime.Now.AddDays(2).AddHours(3),
                Category = new()
                {
                    Id = 1,
                    PriceBands = new()
                    {
                        new()
                        {
                            MinDays = 1,
                            MaxDays = 3,
                            Price = 100
                        }
                    }
                },
                BranchGet = new()
                {
                    Id = 1
                },
                BranchReturn = new()
                {
                    Id = 2
                },
                User = new()
                {
                    Id = 5
                },
                BookExtra = new()
                {
                    new()
                    {
                        Extra = new()
                        {
                            Id = 1,
                            DayCost = 5
                        }
                    }
                }
            };

            var validateFake = Substitute.For<IValidateBooking>();
            validateFake.Validate(booking);

            var dbAccessCategoriesFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessCategoriesFake.GetCategoryById(booking.Category.Id).Returns(booking.Category);

            var dbAccessBranchesFake = Substitute.For<IBranchesDBAccess>();
            dbAccessBranchesFake.GetBranchById(booking.BranchGet.Id).Returns(booking.BranchGet);
            dbAccessBranchesFake.GetBranchById(booking.BranchReturn.Id).Returns(booking.BranchReturn);

            var dbAccessUserFake = Substitute.For<IUserDBAccess>();
            dbAccessUserFake.GetUserByIdThenInclude(booking.User.Id).Returns(booking.User);

            List<Extraa> extras = new() { booking.BookExtra[0].Extra };
            var dbAccessExtrasFake = Substitute.For<IExtraDBAccess>();
            dbAccessExtrasFake.GetExtraDB(booking).Returns(extras);

            var dbAccessBookingFake = Substitute.For<IBookingDBAccess>();
            dbAccessBookingFake.AddNewBook(booking);

            BookingService bookingService = new(dbAccessBookingFake, validateFake, null, dbAccessCategoriesFake,
                dbAccessBranchesFake, dbAccessUserFake, dbAccessExtrasFake);

            bookingService.InsertNewBook(booking);

            dbAccessBookingFake.Received().AddNewBook(Arg.Is<Booking>(b => b.ValueToPay == 315));
        }


        [Fact]
        public void TesteBlabla()
        {
            var service = new BookingService(null, null, null, null, null, null, null);

            var booking1 = new Booking()
            {
                BranchGet = new Branchs() { Id = 1 },
                Category = new Categories() { Id = 1 },
                StartDay = new DateTime(2023, 10, 20, 09, 00, 00),
                ReturnDay = new DateTime(2023, 10, 22, 09, 00, 00),
                BookExtra = new List<BookingExtra>()
            };

            var booking2 = new Booking()
            {
                BranchGet = new Branchs() { Id = 1 },
                Category = new Categories() { Id = 1 },
                StartDay = new DateTime(2023, 10, 20, 09, 00, 00),
                ReturnDay = new DateTime(2023, 10, 22, 09, 00, 00),
                BookExtra = new List<BookingExtra>()
                {
                    new BookingExtra()
                    {
                        Id = 1,
                        Extra = new Extraa()
                        {
                            DayCost = 8,
                            FixedCost = 0
                        }
                    }
                }
            };

            service.InsertNewBook(booking1);
            service.InsertNewBook(booking2);

            booking1.ValueToPay.Should().Be(100);
            booking1.ValueToPay.Should().Be(116);
        }
    }
}
