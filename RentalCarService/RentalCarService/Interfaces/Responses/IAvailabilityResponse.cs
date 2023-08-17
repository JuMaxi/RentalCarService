using RentalCarService.Models;
using RentalCarService.Models.Responses;
using System;
using System.Collections.Generic;

namespace RentalCarService.Interfaces.Responses
{
    public interface IAvailabilityResponse
    {
        public List<Car> CompareAvailability(Availability availability);
    }
}
