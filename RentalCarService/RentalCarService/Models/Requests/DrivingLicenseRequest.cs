using System;

namespace RentalCarService.Models.Requests
{
    public class DrivingLicenseRequest
    {
        public string Number { get; set; }
        public int IssuingCountry { get; set; }
        public DateTime IssuingDate { get; set; }
    }
}
