using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IPriceBandsService
    {
        public void RegistryPricesPerCategory(Categories PriceBands);
        public void UpdatePricePerCategory(Categories Prices);
        public void DeletePricePerCategoryId(int Id);
        public void DeletePricePerCategoryFullCategory(string CodeCategory);
        public List<Categories> ReadPriceBandsPerCategoryDB();
    }
}
