using System;
using System.Collections.Generic;
using System.Globalization;

namespace RentalCarService.Models
{
    public class OpeningHours
    {
        public TimeOnly Opens { get; set; } //Opens
        public TimeOnly Closes { get; set; } //Closes
        public DayOfWeek DayOfWeek { get; set; }

       
    }
}
