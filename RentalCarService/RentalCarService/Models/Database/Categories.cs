using System.Collections.Generic;

namespace RentalCarService.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<PriceBands> PriceBands { get; set; }

    }
}
