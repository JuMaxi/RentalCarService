using FluentAssertions;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReturnsExtensions;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using RentalCarService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
