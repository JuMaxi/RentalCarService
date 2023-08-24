using RentalCarService.Models.Requests;
using RentalCarService.Models;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICarMapper
    {
        public Car ConvertCarRequest(CarRequest carRequest);
        public List<CarResponse> ConvertCarResponse(List<Car> cars);
    }
}
