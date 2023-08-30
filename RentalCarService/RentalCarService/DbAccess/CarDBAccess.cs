using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class CarDBAccess : ICarDBAccess
    {
        readonly RentalCarsDBContext _dbcontext;

        public CarDBAccess(RentalCarsDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Car GetCarByCategoryId(int id)
        {
            var car = _dbcontext.Fleet.Include(c => c.Category).Where(f => f.Category.Id == id).FirstOrDefault();

            return car;
        }

        public int GetCountFleetByCategoryId(int id)
        {
            int count = _dbcontext.Fleet.Include(c => c.Category)
                .Where(f => f.Category.Id == id)
                .Count();

            return count;
        }
    }
}
