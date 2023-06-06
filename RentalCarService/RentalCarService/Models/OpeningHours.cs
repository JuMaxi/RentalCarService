using System;

namespace RentalCarService.Models
{
    public class OpeningHours
    {
        public int Id { get; set; }
        public TimeOnly Opens { get; set; } //Opens
        public TimeOnly Closes { get; set; } //Closes
        public DayOfWeek DayOfWeek { get; set; }

       
    }
}
