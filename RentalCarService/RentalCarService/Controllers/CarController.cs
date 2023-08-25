using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CarController : ControllerBase
    {
        readonly ICarService _carService;
        readonly ICarMapper _carMapper;
        public CarController(ICarService carservice, ICarMapper carMapper)
        {
            _carService = carservice;
            _carMapper = carMapper;
        }

        [HttpPost]
        public void InsertNewCarDB(CarRequest carRequest)
        {
            Car newCar = _carMapper.ConvertCarRequest(carRequest);
            _carService.InsertNewCar(newCar);
        }

        [HttpGet]
        public List<CarResponse> ReadFleetFromDB()
        {
            List<Car> Fleet = _carService.ReadFleetFromDB();
            List<CarResponse> fleetResponse = _carMapper.ConvertCarResponse(Fleet);
            return fleetResponse;
        }

        [HttpDelete]
        public void DeleteCarDB([FromQuery] int Id)
        {
            _carService.DeleteCarFleet(Id);
        }

        [HttpPut("{id}")]
        public void UpdataCarDb(CarRequest carRequest, int id)
        {
            Car car= _carMapper.ConvertCarRequest(carRequest);
            car.Id= id;

            _carService.UpdateCarFleet(car);
        }
        
    }
}
