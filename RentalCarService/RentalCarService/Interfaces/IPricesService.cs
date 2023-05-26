using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IPricesService
    {
        public void RegistryPricesPerCategory(CategoriesPrices Prices);
        public void UpdatePricePerCategory(CategoriesPrices Prices);
        public void DeletePricePerCategoryId(int Id);
        public void DeletePricePerCategoryFullCategory(string CodeCategory);
    }
}
