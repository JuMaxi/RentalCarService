using System;

namespace RentalCarService.Models.Responses
{
    public class AvailabilityResponse
    {
        public string Branch { get; set; }
        public string Category { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDay { get; set; }
        public double Estimative { get; set; }

    }
}
