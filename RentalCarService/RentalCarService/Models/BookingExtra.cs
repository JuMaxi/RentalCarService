namespace RentalCarService.Models
{
    public class BookingExtra
    {
        public int Id { get; set; }
        public Booking Book { get; set; }
        public Extraa Extra { get; set; }
    }
}
