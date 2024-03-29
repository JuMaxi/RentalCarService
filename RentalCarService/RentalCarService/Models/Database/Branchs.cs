﻿using System.Collections.Generic;

namespace RentalCarService.Models
{
    public class Branchs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Countries Country { get; set; }
        public string Address { get; set; }
        public List<OpeningHours> OpeningHours { get; set; }
    }
}
