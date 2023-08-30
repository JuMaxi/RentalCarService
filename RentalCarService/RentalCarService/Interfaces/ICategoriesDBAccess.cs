using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface ICategoriesDBAccess
    {
        public Categories GetCategoryByCode(string code);
    }
}
