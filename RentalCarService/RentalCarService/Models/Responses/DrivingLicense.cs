using System;

namespace RentalCarService.Models.Responses
{
    public class DrivingLicense
    {
        public string Number { get; set; }
        public string IssuingCountry { get; set; }
        public DateTime IssuingDate { get; set; }
    }
}
