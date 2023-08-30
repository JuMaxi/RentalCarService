using FluentAssertions;
using RentalCarService.Models;
using RentalCarService.Validators;

namespace RentarlCars.Tests.Validators
{
    public class ValidateBranchesTests
    {
        [Fact]
        public void When_the_branch_name_is_null_should_throw_exception()
        {
            Branchs branch = new()
            {
                Name = null
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Name must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_length_branch_name_is_zero_should_throw_exception()
        {
            Branchs branch = new()
            {
                Name = ""
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Name must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_phone_is_null_should_throw_new_exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = null
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Phone must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_length_phone_is_zero_should_throw_new_Exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = ""
            };

            ValidateBranches validateBranch = new();

            validateBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Phone must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_country_id_is_zero_should_throw_new_exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-4758",
                Country = new() { Id = 0 }
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field CountryId must be filled with a valid Country Id to continue");
        }

        [Fact]
        public void When_the_address_is_null_should_throw_new_exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2536",
                Country = new() { Id = 1 },
                Address = null
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Address must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_length_address_is_null_should_throw_new_exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 4152-2341",
                Country = new() { Id = 1 },
                Address = ""
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().Throw<Exception>()
                .WithMessage("The field Address must be filled to continue and can't be null.");
        }

        [Fact]
        public void When_the_validator_branch_doesnot_have_exception()
        {
            Branchs branch = new()
            {
                Name = "Paula Fox",
                Phone = "(44) 2536-6547",
                Country = new() { Id = 1 },
                Address = "Love Street, 27, London, UK"
            };

            ValidateBranches validatorBranch = new();

            validatorBranch.Invoking(validator => validator.ValidateBranch(branch))
                .Should().NotThrow<Exception>();
        }


    }
}
