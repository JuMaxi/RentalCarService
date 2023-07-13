using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateCategories : IValidateCategories
    {
        private readonly RentalCarsDBContext _dbContext;
        public ValidateCategories(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ValidateCategory(Categories Category)
        {
            if (Category.Code == null
                || Category.Code.Length == 0)
            {
                throw new Exception("The Code of de Car must be filled to continue.");
            }
            ValidateUniqueCodeCategory(Category);
            ValidateDescriptionCategory(Category);
            ValidatePriceBands(Category);
        }
        private void ValidateUniqueCodeCategory(Categories Category)
        {
            Categories categoriefromDataBase = _dbContext.Categories.Where(c => c.Code == Category.Code).FirstOrDefault();

            if(categoriefromDataBase != null)
            {
                throw new Exception("The Code Category must be unique. Change the code to continue.");
            }
        }
        private void ValidateDescriptionCategory(Categories Category)
        {
            if (Category.Description == null
                    || Category.Description.Length == 0)
            {
                throw new Exception("The Description of the car must be filled to continue.");
            }
        }

        private void ValidatePriceBands(Categories PriceBands)
        {
            if (PriceBands.PriceBands.Any(price => price.MinDays == 0 || price.MinDays < 0))
            {
                throw new Exception("The Min Days must be filled with value different than zero, null or empty");
            }

            if (PriceBands.PriceBands.Any(price => price.MaxDays == 0 || price.MaxDays < 0))
            {
                throw new Exception("The Max Days must be filled with value different than zero, null or empty.");
            }

            if (PriceBands.PriceBands.Any(price => price.Price == 0 || price.Price < 0))
            {
                throw new Exception("The Price must be filled to continue and must be greater than zero");
            }
        }
    }
}
