namespace RentalCarService.Models.Requests
{
    public class UserAddressRequest
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public int Country { get; set; }
    }
}
