using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateCategories : IValidateCategories
    {
        private readonly ICategoriesDBAccess _categoriesDBAccess;
        public ValidateCategories(ICategoriesDBAccess categoriesDBAccess)
        {
            _categoriesDBAccess = categoriesDBAccess;
        }

        public void ValidateCategory(Categories Category)
        {
            if (Category.Code is null
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
            Categories categoriefromDataBase = _categoriesDBAccess.GetCategoryByCode(Category.Code);

            if (categoriefromDataBase is not null)
            {
                throw new Exception("The Code Category must be unique. Change the code to continue.");
            }
        }
        private void ValidateDescriptionCategory(Categories Category)
        {
            if (Category.Description is null
                    || Category.Description.Length == 0)
            {
                throw new Exception("The Description of the category must be filled to continue.");
            }
        }

        private void ValidatePriceBands(Categories PriceBands)
        {
            foreach (PriceBands Price in PriceBands.PriceBands)
            {
                if (Price.MinDays == 0 || Price.MinDays < 0)
                {
                    throw new Exception("The Min Days must be filled with value different than zero, null or empty");
                }

                if (Price.MaxDays == 0 || Price.MaxDays < 0)
                {
                    throw new Exception("The Max Days must be filled with value different than zero, null or empty.");
                }

                if (Price.Price == 0 || Price.Price < 0)
                {
                    throw new Exception("The Price must be filled to continue and must be greater than zero");
                }
            }

        }
    }
}
