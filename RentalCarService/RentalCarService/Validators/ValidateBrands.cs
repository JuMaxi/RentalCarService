using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateBrands : IValidateBrands
    {
        private readonly RentalCarsDBContext _dbContext;
        public ValidateBrands(RentalCarsDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void ValidateBrandName(Brands Brand)
        {
            if (Brand.Brand == null || Brand.Brand.Length == 0)
            {
                throw new Exception("The Brand must be filled and can't be null. Fill the field to continue.");
            }
            ValidateRepeatedBrandName(Brand);
        }
        private void ValidateRepeatedBrandName(Brands Brand)
        {
            Brands brandsFromDatabase = _dbContext.Brands.Where(b => b.Brand.Equals(Brand.Brand)).FirstOrDefault();

            if (brandsFromDatabase != null)
            {
                throw new Exception("The Brand " + Brand.Brand + " already exist in this DataBase, insert a different Brand to continue.");
            }
        }
    }
}
