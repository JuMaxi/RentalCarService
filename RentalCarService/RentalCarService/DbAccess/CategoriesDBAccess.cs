using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Data.Common;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class CategoriesDBAccess : ICategoriesDBAccess
    {
        readonly RentalCarsDBContext _dbContext;

        public CategoriesDBAccess(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Categories GetCategoryByCode(string code)
        {
            return _dbContext.Categories.Where(c => c.Code == code).FirstOrDefault();
        }


    }
}
