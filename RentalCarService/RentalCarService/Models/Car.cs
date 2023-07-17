namespace RentalCarService.Models
{
    public class Car
    {
        public int Id { get; set; }
        public Brands Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string Doors { get; set; }
        public string Seats { get; set; }
        public string AirConditioner { get; set; }
        public int TrunkSize { get; set; }
        public string NumberPlate { get; set; }
        public Categories Category { get; set; }

    }
}
