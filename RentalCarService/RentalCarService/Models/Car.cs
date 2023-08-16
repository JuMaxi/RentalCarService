using System;

namespace RentalCarService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Brands Brand { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Transmission { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public string AirConditioner { get; set; }
        public int TrunkSize { get; set; }
        public string NumberPlate { get; set; }
        public Categories Category { get; set; }
        public Branchs Branch { get; set; }

    }
}
