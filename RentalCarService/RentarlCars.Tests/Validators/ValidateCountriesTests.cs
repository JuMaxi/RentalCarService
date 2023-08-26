using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;

namespace RentarlCars.Tests.Validators
{
    public class ValidateCountriesTests
    {
        [Fact]
        public void When_country_is_null_should_throw_new_exception()
        {
            Countries country = new()
            {
                Country = null
            };

            ValidateCountries validatorCountry = new();

            validatorCountry.Invoking(validator => validator.ValidateNameCountry(country.Country))
                .Should().Throw<Exception>()
                .WithMessage("The name of country must be filled to continue.");
        }

        [Fact]
        public void When_country_length_is_zero_should_throw_new_exception()
        {
            Countries country = new()
            {
                Country = ""
            };

            ValidateCountries validatorCountry = new();

            validatorCountry.Invoking(validator => validator.ValidateNameCountry(country.Country))
                .Should().Throw<Exception>()
                .WithMessage("The name of country must be filled to continue.");
        }

        [Fact]
        public void When_country_doesnot_have_exception()
        {
            Countries country = new()
            {
                Country = "Brazil"
            };

            ValidateCountries validatorCountries = new();

            validatorCountries.Invoking(validator => validator.ValidateNameCountry(country.Country))
                .Should().NotThrow<Exception>();
        }
    }
}
