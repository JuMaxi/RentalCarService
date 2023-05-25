using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICategoriesService
    {
        public void RegistryNewCategory(Categories NewCategory);
        public List<Categories> ReadCategoriesFromDB();
    }
}
