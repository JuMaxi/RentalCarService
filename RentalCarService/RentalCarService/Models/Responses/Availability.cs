using System;

namespace RentalCarService.Models.Responses
{
    public class Availability
    {
        public Categories Category { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Estimative { get; set; }

    }
}
