using System;

namespace RentalCarService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string IdentityDocument { get; set; }
        public UserAddress Address { get; set; }
        public DateTime Birthday { get; set; }
        public Countries Nationality { get; set; }
        public string Gender { get; set; }
        public string CNH { get; set; }
        public Countries CountryCNH { get; set; }
        public DateTime DateCNH { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
