using System;
using System.Collections.Generic;

namespace RentalCarService.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public User User { get; set; }
        public Categories Category { get; set; }
        public Branchs BranchGet { get; set; }
        public Branchs BranchReturn { get; set; }
        public List<BookExtra> BookExtra { get; set; }
        public double ValueToPay { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime ReturnDay { get; set; }
        public TimeOnly HourGetCar { get; set; }
        public TimeOnly HourReturnCar { get; set; } 
    }
}
