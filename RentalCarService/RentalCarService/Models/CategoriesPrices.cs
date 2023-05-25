using Microsoft.AspNetCore.SignalR;

namespace RentalCarService.Models
{
    public class CategoriesPrices
    {
        public int CategorieId { get; set; }
        public string CodeCategory { get; set; }
        public string MixDays { get; set; }
        public string MaxDays { get; set; } 
        public int Price { get; set; }
    }
}
