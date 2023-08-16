using System;

namespace RentalCarService.Models.Responses
{
    public class Availability
    {
        public int BranchGetCar { get; set; }
        public Categories Category { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDay { get; set; }
        public double Estimative { get; set; }

    }
}
