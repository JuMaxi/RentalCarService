using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarController : ControllerBase
    {
        ICarService CarService;
        public CarController(ICarService carservice) 
        {
            CarService = carservice;
        }

        [HttpPost]
        public void InsertNewCarDB(CarRequest carRequest)
        {
            Car newCar= ConvertCarRequest(carRequest);
            CarService.InsertNewCar(newCar);
        }
        [HttpGet]
        public List<Car> ReadFleetFromDB()
        {
            List<Car> Fleet = CarService.ReadFleetFromDB();
            return Fleet;
        }

        [HttpDelete]
        public void DeleteCarDB([FromQuery] int Id)
        {
            CarService.DeleteCarFleet(Id);
        }

        [HttpPut]
        public void UpdataCarDb(Car Car)
        {
            CarService.UpdateCarFleet(Car);
        }

        private Car ConvertCarRequest(CarRequest carRequest)
        {
            Car car= new Car();

            Brands brand= new Brands();
            brand.Id= carRequest.BrandId;
            car.Brand= brand;

            car.Model= carRequest.Model;
            car.Year= carRequest.Year;
            car.Transmission= carRequest.Transmission;
            car.Doors= carRequest.Doors;
            car.Seats= carRequest.Seats;
            car.AirConditioner= carRequest.AirConditioner;
            car.TrunkSize= carRequest.TrunkSize;
            car.NumberPlate= carRequest.NumberPlate;

            Categories category= new Categories();
            category.Id= carRequest.CategoryId;
            car.Category= category;

            Branchs branch= new Branchs();
            branch.Id=carRequest.BranchId;
            car.Branch= branch;

            return car;
        }
    }
}
