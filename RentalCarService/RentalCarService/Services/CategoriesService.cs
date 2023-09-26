using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;


namespace RentalCarService.Services
{
    public class CategoriesService : ICategoriesService
    {
        IValidateCategories ValidateCategories;
        private readonly ICategoriesDBAccess _categoriesDBAccess;

        public CategoriesService(IValidateCategories validateCategories, ICategoriesDBAccess categoriesDBAccess)
        {
            ValidateCategories = validateCategories;
            _categoriesDBAccess = categoriesDBAccess;
        }
        public void RegistryNewCategory(Categories NewCategory)
        {
            ValidateCategories.ValidateCategory(NewCategory);
            
            _categoriesDBAccess.AddNewCategory(NewCategory);
        }

        public List<Categories> ReadCategoriesFromDB()
        {
            return _categoriesDBAccess.GetCategories();
        }

        private Categories FindPriceBandsDB(int CategoryId)
        {
            return _categoriesDBAccess.GetCategoryById(CategoryId);
        }

        public void DeleteCategory(int Id)
        {
            _categoriesDBAccess.DeleteCategory(Id);
        }

        public void UpdateCategory(Categories Category)
        {
            ValidateCategories.ValidateCategory(Category);

            Categories toUpdate = _categoriesDBAccess.GetCategoryById(Category.Id);

            toUpdate.Code = Category.Code;
            toUpdate.Description = Category.Description;
            toUpdate.PriceBands = Category.PriceBands;
            
            _categoriesDBAccess.UpdateCategory(toUpdate);
        }
    }
}
