using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IBrandsService
    {
        public void InserNewBrand(Brands Brand);
        public List<Brands> ReadBrandsFromDB();
        public void DeleteBrand(int Id);
        public void UpdateBrand(Brands Brand);
    }
}
