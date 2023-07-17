using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICarService
    {
        public void InsertNewCar(Car Car);
        public List<Car> ReadFleetFromDB();
        public void DeleteCarFleet(int Id);
        public void UpdateCarFleet(Car Car);
    }
}
