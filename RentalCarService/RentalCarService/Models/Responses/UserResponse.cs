using System;

namespace RentalCarService.Models.Responses
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdentityDocument { get; set; }
        public UserAddressResponse Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public DrivingLicense DriverLicense { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
