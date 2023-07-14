using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
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

        public void DeleteCategory(int Id)
        {
            Categories toRemove = _dbContext.Categories.Include(P => P.PriceBands).Where(c => c.Id == Id).FirstOrDefault();

            foreach (var priceband in toRemove.PriceBands)
                _dbContext.Remove(priceband);

            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }
    }
}
