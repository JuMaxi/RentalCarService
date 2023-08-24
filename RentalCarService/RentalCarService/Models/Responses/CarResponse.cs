using System;

namespace RentalCarService.Models.Responses
{
    public class CarResponse
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Transmission { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public string AirConditioner { get; set; }
        public int TrunkSize { get; set; }
        public string NumberPlate { get; set; }
        public string Category { get; set; }
        public string Branch { get; set; }
    }
}
