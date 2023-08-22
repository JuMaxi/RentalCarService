using System;

namespace RentalCarService.Models.Requests
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdentityDocument { get; set; }
        public UserAddressRequest Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Nationality { get; set; }
        public string Gender { get; set; }
        public DrivingLicenseRequest DriverLicense { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
