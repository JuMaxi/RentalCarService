using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class CarService : ICarService
    {
        IValidateCar ValidateCar;
        private readonly RentalCarsDBContext _dbContext;
        public CarService(IValidateCar validatecar, RentalCarsDBContext dbContext) 
        {
            ValidateCar = validatecar;
            _dbContext= dbContext;
        }

        public void InsertNewCar(Car Car)
        {
            ValidateCar.Validate(Car);

            Car.Brand = FindBrandIdDB(Car.Brand.Id);
            Car.Category = FindCategoryIdDB(Car.Category.Id);
            Car.Branch = FindBranchFromDB(Car.Branch.Id);

            _dbContext.Add(Car);
            _dbContext.SaveChanges();
        }

        public List<Car> ReadFleetFromDB()
        {
            var Fleet = _dbContext.Fleet.Include(B => B.Brand).ToList();
            Fleet = _dbContext.Fleet.Include(C => C.Category.PriceBands).ToList();
         
            return Fleet;
        }
        public void DeleteCarFleet(int Id)
        {
            Car toRemove = _dbContext.Fleet.Find(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }
        public void UpdateCarFleet(Car Car)
        {
            ValidateCar.Validate(Car);

            Car toUpdate = _dbContext.Fleet.Find(Car.Id);
            toUpdate.Brand = FindBrandIdDB(Car.Brand.Id);
            toUpdate.Model = Car.Model;
            toUpdate.Year = Car.Year;
            toUpdate.Transmission = Car.Transmission;
            toUpdate.Doors = Car.Doors;
            toUpdate.Seats = Car.Seats;
            toUpdate.AirConditioner = Car.AirConditioner;
            toUpdate.TrunkSize = Car.TrunkSize;
            toUpdate.NumberPlate = Car.NumberPlate;
            toUpdate.Category = FindCategoryIdDB(Car.Category.Id);

            _dbContext.SaveChanges();
        }
        private Brands FindBrandIdDB(int Id)
        {
            Brands Brand = _dbContext.Brands.Where(I => I.Id == Id).FirstOrDefault();
            return Brand;
        }
        private Categories FindCategoryIdDB(int Id)
        {
            Categories Category = _dbContext.Categories.Where(I => I.Id == Id).FirstOrDefault();
            return Category;
        }
        private Branchs FindBranchFromDB(int Id)
        {
            Branchs branch = _dbContext.Branches.Where(I => I.Id == Id).FirstOrDefault();
            return branch;
        }
    }
}
