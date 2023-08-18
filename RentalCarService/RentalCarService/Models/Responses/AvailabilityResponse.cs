using System;

namespace RentalCarService.Models.Responses
{
    public class AvailabilityResponse
    {
        public Branchs Branch { get; set; }
        public Categories Category { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDay { get; set; }
        public double Estimative { get; set; }

    }
}
