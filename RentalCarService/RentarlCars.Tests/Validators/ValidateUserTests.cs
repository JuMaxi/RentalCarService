using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests.Validators
{
    public class ValidateUserTests
    {
        [Fact]
        public void When_name_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The name must be filled and can't be null.");
        }

        [Fact]
        public void When_name_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The name must be filled and can't be null.");
        }

        [Fact]
        public void When_phone_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Phone must be filled and can't be null.");
        }

        [Fact]
        public void When_phone_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Phone must be filled and can't be null.");
        }

        [Fact]
        public void When_identitydocument_is_null_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Identity Document must be filled and can't be null.");
        }

        [Fact]
        public void When_identitydocumento_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Identity Document must be filled and can't be null.");
        }

        [Fact]
        public void When_birthday_year_is_greater_than_current_year_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "2014-2541-7463",
                Birthday = DateTime.Now.AddYears(1)
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The year can't be greater or equal the actual year.");
        }

        [Fact]
        public void When_nationality_id_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 0 }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Nationality Country Id must be filled and can't be null.");
        }

        [Fact]
        public void When_gender_is_null_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Gender must be filled and can't be null.");
        }

        [Fact]
        public void When_gender_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Gender must be filled and can't be null.");
        }

        [Fact]
        public void When_CNH_is_null_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = null
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH must be filled and can't be null.");
        }

        [Fact]
        public void When_CNH_length_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = ""
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH must be filled and can't be null.");
        }

        [Fact]
        public void When_countryCNH_id_is_zero_should_throw_new_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new() { Id = 1 },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new() { Id = 0 }
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The CNH Country Id must be filled and can't be null.");
        }

        [Fact]
        public void When_dateCNH_year_is_greater_than_current_year_should_throw_exception()
        {
            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
                DateCNH = DateTime.Now.AddYears(1),
            };

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Date of the CNH must be less than the current year and greater than the year of user's birthday plus 18.");
        }

        [Fact]
        public void When_dateCNH_is_less_than_birthday_plus18years_should_throw_new_exception()
        {

            User user = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                IdentityDocument = "1014-2536-5285",
                Birthday = new DateTime(1985, 01, 01),
                Nationality = new()
                {
                    Id = 1
                },
                Gender = "Female",
                CNH = "1254-4785-2347",
                CountryCNH = new()
                {
                    Id = 1
                },
            };
            user.DateCNH = user.Birthday.AddYears(17);

            ValidateUser validatorUser = new();

            validatorUser.Invoking(validator => validator.Validate(user))
                .Should().Throw<Exception>()
                .WithMessage("The Date of the CNH must be less than the current year and greater than the year of user's birthday plus 18.");
        }
    }
}
