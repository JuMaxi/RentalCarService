using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class BrandsService : IBrandsService
    {
        IValidateBrands ValidateBrands;
        private readonly RentalCarsDBContext _dbContext;
        public BrandsService(IValidateBrands validateBrands, RentalCarsDBContext dbContext)
        {
            ValidateBrands = validateBrands;
            _dbContext = dbContext;
        }
        public void InserNewBrand(Brands Brand) 
        {
            ValidateBrands.ValidateBrandName(Brand);
            _dbContext.Brands.Add(Brand);
            _dbContext.SaveChanges();
        }

        public List<Brands> ReadBrandsFromDB()  
        {
            var allBrands = _dbContext.Brands.ToList();
            return allBrands;
        }
        public void DeleteBrand(int Id)
        {
            Brands toRemove = _dbContext.Brands.Find(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateBrand(Brands Brand)
        {
            ValidateBrands.ValidateBrandName(Brand);
            Brands toUpdate = _dbContext.Brands.Find(Brand.Id);
            toUpdate.Brand = Brand.Brand;
            _dbContext.SaveChanges();
        }
    }
}
