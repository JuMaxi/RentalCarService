using System;

namespace RentalCarService.Models.Requests
{
    public class AvailabilityRequest
    {
        public int BranchGetCar { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDay { get; set; }
    }
}
