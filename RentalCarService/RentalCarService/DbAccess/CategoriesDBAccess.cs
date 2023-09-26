using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Validators;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class CategoriesDBAccess : ICategoriesDBAccess
    {
        readonly RentalCarsDBContext _dbContext;

        public CategoriesDBAccess(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Categories GetCategoryByCode(string code)
        {
            return _dbContext.Categories.Where(c => c.Code == code).FirstOrDefault();
        }

        public void AddNewCategory(Categories NewCategory)
        {
            _dbContext.Categories.Add(NewCategory);
            _dbContext.SaveChanges();
        }

        public List<Categories> GetCategories()
        {
            var allCategories = _dbContext.Categories.Include(c => c.PriceBands).ToList();
            return allCategories;
        }

        public Categories GetCategoryById(int id)
        {
            Categories Categorie = _dbContext.Categories.Include(P => P.PriceBands).Where(c => c.Id == id).FirstOrDefault();

            return Categorie;
        }

        public void DeleteCategory(int id)
        {
            Categories toRemove = GetCategoryById(id);

            foreach (var priceband in toRemove.PriceBands)
                _dbContext.Remove(priceband);

            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Categories category)
        {
            _dbContext.Categories.Update(category);            
            _dbContext.SaveChanges();
        }
    }
}
