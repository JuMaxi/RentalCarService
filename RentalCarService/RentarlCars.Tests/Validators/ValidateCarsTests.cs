using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;

namespace RentarlCars.Tests.Validators
{
    public class ValidateCarsTests
    {
        [Fact]
        public void When_model_is_null_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = null
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Model Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_model_length_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = ""
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Model Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_year_is_greater_than_actual_year_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2024, 01, 01)
            };

            ValidateCar validatorCar = new();
            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Year must less or equal the current year.");
        }

        [Fact]
        public void When_transmition_is_null_should_throw_new_Exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = null
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Transmission Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_transmition_length_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = ""
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Transmission Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_doors_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monz",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 0
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Doors Car must be greater than zero.");
        }

        [Fact]
        public void When_doors_is_less_than_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = -1
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Doors Car must be greater than zero.");
        }

        [Fact]
        public void When_seats_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 0
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Seats Car must be greater than zero.");
        }

        [Fact]
        public void When_seats_is_less_than_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = -1
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Seats Car must be greater than zero.");
        }

        [Fact]
        public void When_airconditioner_is_null_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = null
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The AirConditioner must be filled to continue and can't be null");
        }

        [Fact]
        public void When_airconditioner_lenght_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = ""
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The AirConditioner must be filled to continue and can't be null");
        }

        [Fact]
        public void When_trunksize_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = 0
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Trunk Size must be greater than zero.");
        }

        [Fact]
        public void When_trunksize_is_less_than_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = -1
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Trunk Size must be greater than zero.");
        }

        [Fact]
        public void When_numberplate_is_null_shoudl_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = 4,
                NumberPlate = null
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Number Plate Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_numberplate_length_is_zero_should_throw_new_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = 4,
                NumberPlate = ""
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().Throw<Exception>()
                .WithMessage("The Number Plate Car must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_validate_car_doesnot_have_exception()
        {
            Car car = new()
            {
                Model = "Monza",
                Year = new DateTime(2023, 01, 01),
                Transmission = "Automatic",
                Doors = 5,
                Seats = 5,
                AirConditioner = "S",
                TrunkSize = 4,
                NumberPlate = "KIJ 4L52"
            };

            ValidateCar validatorCar = new();

            validatorCar.Invoking(validator => validator.Validate(car))
                .Should().NotThrow<Exception>();
        }
    }
}
