using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace RentalCarService.Services
{
    public class CategoriesService : ICategoriesService
    {
        IValidateCategories ValidateCategories;
        private readonly RentalCarsDBContext _dbContext;

        public CategoriesService(IValidateCategories validateCategories, RentalCarsDBContext dbContext)
        {
            ValidateCategories = validateCategories;
            _dbContext = dbContext;
        }
        public void RegistryNewCategory(Categories NewCategory)
        {
            ValidateCategories.ValidateCategory(NewCategory);
            _dbContext.Categories.Add(NewCategory);
            _dbContext.SaveChanges();
        }

        public List<Categories> ReadCategoriesFromDB()
        {
            var allCategories = _dbContext.Categories.Include(c => c.PriceBands).ToList();
            return allCategories;
        }

        private Categories FindPriceBandsDB(int CategoryId)
        {
            Categories Categorie = _dbContext.Categories.Include(P => P.PriceBands).Where(c => c.Id == CategoryId).FirstOrDefault();

            foreach (var priceband in Categorie.PriceBands)
                _dbContext.Remove(priceband);

            return Categorie;
        }
        public void DeleteCategory(int Id)
        {
            Categories toRemove = FindPriceBandsDB(Id);

            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Categories Category)
        {
            ValidateCategories.ValidateCategory(Category);

            Categories toUpdate = FindPriceBandsDB(Category.Id);

            toUpdate.Code = Category.Code;
            toUpdate.Description = Category.Description;
            toUpdate.PriceBands = Category.PriceBands;
            _dbContext.SaveChanges();
        }
    }
}
