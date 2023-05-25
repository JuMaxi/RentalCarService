using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IInsertCategories
    {
        public void RegistryNewCategory(Categories NewCategory);
        public List<Categories> ReadCategoriesFromDB();
    }
}
