using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;

namespace RentarlCars.Tests.Validators
{
    public class ValidateExtraTests
    {
        [Fact]
        public void When_service_is_null_should_throw_new_exception()
        {
            Extraa extra = new()
            {
                Service = null,
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("The service/product can't be empty or null.");
        }

        [Fact]
        public void When_service_length_is_zero_should_throw_new_exception()
        {
            Extraa extra = new()
            {
                Service = ""
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("The service/product can't be empty or null.");
        }

        [Fact]
        public void When_daycost_is_less_than_zero_should_throw_new_exception()
        {
            Extraa extra = new()
            {
                Service = "GPS",
                DayCost = -1
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("The cost must be greater than zero.");
        }

        [Fact]
        public void When_fixedcost_is_less_than_zero_should_throw_new_exception()
        {
            Extraa extra = new()
            {
                Service = "GPS",
                DayCost = 0,
                FixedCost = -1
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("The cost must be greater than zero.");
        }

        [Fact]
        public void When_daycost_and_fixedcost_are_zero_should_throw_exception()
        {
            Extraa extra = new()
            {
                Service = "GPS",
                DayCost = 0,
                FixedCost = 0,
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("At least one field cost must be filled with a value greater than zero to continue.");
        }

        [Fact]
        public void When_daycost_and_fixedcost_are_greater_than_zero_should_throw_new_exception()
        {
            Extraa extra = new()
            {
                Service = "GPS",
                DayCost = 10,
                FixedCost = 40
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().Throw<Exception>()
                .WithMessage("Just one field cost must be filled.");
        }

        [Fact]
        public void When_extra_doesnot_have_exception()
        {
            Extraa extra = new()
            {
                Service = "GPS",
                DayCost = 0,
                FixedCost = 35
            };

            ValidateExtra validatorExtra = new();

            validatorExtra.Invoking(validator => validator.Validate(extra))
                .Should().NotThrow<Exception>();
        }

    }
}
