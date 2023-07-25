using System;

namespace RentalCarService.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public User User { get; set; }
        public Categories Category { get; set; }
        public Branchs Branch { get; set; }
        public Extraa Extra { get; set; }
        public int ValueToPay { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime FinishDay { get; set; }
    }
}
