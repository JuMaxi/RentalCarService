using FluentAssertions;
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
    public class ValidateBrandsTests
    {
        [Fact]
        public void When_brand_is_null_should_throw_new_exception()
        {
            Brands brand = new()
            {
                Brand = null
            };

            ValidateBrands validatorBrand = new(null);

            validatorBrand.Invoking(validator => validator.ValidateBrandName(brand))
                .Should().Throw<Exception>()
                .WithMessage("The Brand must be filled and can't be null. Fill the field to continue.");
        }

        [Fact]
        public void When_brand_length_is_zero_should_throw_new_exception()
        {
            Brands brand = new()
            {
                Brand = ""
            };

            ValidateBrands validatorBrand = new(null);

            validatorBrand.Invoking(validator => validator.ValidateBrandName(brand))
                .Should().Throw<Exception>()
                .WithMessage("The Brand must be filled and can't be null. Fill the field to continue.");
        }

        [Fact]
        public void When_brand_already_exists_should_throw_exception()
        {
            Brands brandIWantToInclude = new()
            {
                Brand = "Boots"
            };

            Brands brandAlreadyExists = new()
            {
                Brand = "Boots"
            };

            var dbAccessFake = Substitute.For<IBrandsDBAccess>();
            dbAccessFake.GetBrandByName("Boots").Returns(brandAlreadyExists);

            ValidateBrands validatorBrand = new(dbAccessFake);

            validatorBrand.Invoking(validator => validator.ValidateBrandName(brandIWantToInclude))
                .Should().Throw<Exception>()
                .WithMessage("The Brand Boots already exist in this DataBase, insert a different Brand to continue.");
        }

        [Fact]
        public void When_brand_from_db_is_null_should_not_throw_exception()
        {
            Brands brandIWantToInclude = new()
            {
                Brand = "Boots"
            };

            var dbAccessFake = Substitute.For<IBrandsDBAccess>();
            dbAccessFake.GetBrandByName("Boots").ReturnsNull();

            ValidateBrands validatorBrand = new(dbAccessFake);

            validatorBrand.Invoking(validator => validator.ValidateBrandName(brandIWantToInclude))
                .Should().NotThrow<Exception>();
        }
    }
}
