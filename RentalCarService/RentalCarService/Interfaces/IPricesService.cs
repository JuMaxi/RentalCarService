using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IPricesService
    {
        public void RegistryPricesPerCategory(CategoriesPrices Prices);
    }
}
