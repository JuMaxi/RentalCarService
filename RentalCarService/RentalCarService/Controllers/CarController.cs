using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
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
        public void InsertNewCarDB(Car NewCar)
        {
            CarService.InsertNewCar(NewCar);
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
    }
}
