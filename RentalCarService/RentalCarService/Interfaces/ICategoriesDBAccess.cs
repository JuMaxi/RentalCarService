using Microsoft.EntityFrameworkCore.Update.Internal;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICategoriesDBAccess
    {
        public Categories GetCategoryByCode(string code);
        public void AddNewCategory(Categories category);
        public List<Categories> GetCategories();
        public Categories GetCategoryById(int id);
        public void DeleteCategory(int id);
        public void UpdateCategory(Categories category);
    }
}
