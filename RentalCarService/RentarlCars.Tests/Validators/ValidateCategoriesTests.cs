using FluentAssertions;
using Newtonsoft.Json.Bson;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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
    public class ValidateCategoriesTests
    {
        [Fact]
        public void When_code_is_null_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = null
            };

            ValidateCategories validatorCategory = new(null);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Code of de Car must be filled to continue.");
        }

        [Fact]
        public void When_code_length_is_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = ""
            };

            ValidateCategories validatorCategory = new(null);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Code of de Car must be filled to continue.");
        }

        [Fact]
        public void When_category_already_exists_should_throw_new_exception()
        {
            Categories categoryIWantToInclude = new()
            {
                Code = "E"
            };
            Categories categoryAlreadyExists = new()
            {
                Code = "E"
            };

            var dbAcessFake = Substitute.For<ICategoriesDBAccess>();
            dbAcessFake.GetCategoryByCode(categoryIWantToInclude.Code).Returns(categoryAlreadyExists);

            ValidateCategories validatorCategory = new(dbAcessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(categoryIWantToInclude))
                .Should().Throw<Exception>()
                .WithMessage("The Code Category must be unique. Change the code to continue.");
        }

        [Fact]
        public void When_description_is_null_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = null
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Description of the category must be filled to continue.");
        }

        [Fact]
        public void When_description_length_is_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = ""
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Description of the category must be filled to continue.");
        }

        [Fact]
        public void When_mindays_is_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new List<PriceBands>()
                {
                    new()
                    {
                        MinDays = 0,
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Min Days must be filled with value different than zero, null or empty");
        }

        [Fact]
        public void When_mindays_is_less_than_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = -1
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Min Days must be filled with value different than zero, null or empty");
        }

        [Fact]
        public void When_maxdays_is_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = 1,
                        MaxDays = 0
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Max Days must be filled with value different than zero, null or empty.");
        }

        [Fact]
        public void When_maxdays_is_less_than_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = 1,
                        MaxDays = -1
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Max Days must be filled with value different than zero, null or empty.");
        }

        [Fact]
        public void When_price_is_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = 1,
                        MaxDays = 5,
                        Price = 0
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Price must be filled to continue and must be greater than zero");
        }

        [Fact]
        public void When_price_is_less_than_zero_should_throw_new_exception()
        {
            Categories category = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = 1,
                        MaxDays = 5,
                        Price = -1
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(category.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(category))
                .Should().Throw<Exception>()
                .WithMessage("The Price must be filled to continue and must be greater than zero");
        }

        [Fact]
        public void When_category_is_null_from_db_should_not_throw_exception()
        {
            Categories categoryIWantToInclude = new()
            {
                Code = "E",
                Description = "Economy",
                PriceBands = new()
                {
                    new()
                    {
                        MinDays = 1, 
                        MaxDays = 5,
                        Price = 70
                    }
                }
            };

            var dbAccessFake = Substitute.For<ICategoriesDBAccess>();
            dbAccessFake.GetCategoryByCode(categoryIWantToInclude.Code).ReturnsNull();

            ValidateCategories validatorCategory = new(dbAccessFake);

            validatorCategory.Invoking(validator => validator.ValidateCategory(categoryIWantToInclude))
                .Should().NotThrow<Exception>();
        }
    }
}
