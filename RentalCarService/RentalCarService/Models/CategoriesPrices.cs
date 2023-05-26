using Microsoft.AspNetCore.SignalR;

namespace RentalCarService.Models
{
    public class CategoriesPrices
    {
        public int Id { get; set; }
        public int CategorieId { get; set; }
        public string CodeCategory { get; set; }
        public string MinDays { get; set; }
        public string MaxDays { get; set; } 
        public double Price { get; set; }
    }
}
