using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Data;
using System.Linq;


namespace RentalCarService.DbAccess
{
    public class BrandsDBAccess : IBrandsDBAccess
    {
        private readonly RentalCarsDBContext _dbContext;

        public BrandsDBAccess(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Brands GetBrandByName(string name)
        {
            return _dbContext.Brands.Where(b => b.Brand.Equals(name)).FirstOrDefault();
        }
    }
}
