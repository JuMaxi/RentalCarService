namespace RentalCarService.Models
{
    public class PriceBands
    {
        public int Id { get; set; }
        public int MinDays { get; set; }
        public int MaxDays { get; set; } 
        public double Price { get; set; }

    }
}
