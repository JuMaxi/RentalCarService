using System;

namespace RentalCarService.Models.Requests
{
    public class CarRequest
    {
        public int BrandId { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Transmission { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public string AirConditioner { get; set; }
        public int TrunkSize { get; set; }
        public string NumberPlate { get; set; }
        public int CategoryId { get; set; }
        public int BranchId { get; set; }
    }
}
