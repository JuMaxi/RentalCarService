using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IBrandsDBAccess
    {
        Brands GetBrandByName(string name);
    }
}