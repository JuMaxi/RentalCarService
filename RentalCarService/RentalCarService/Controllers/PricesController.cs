using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PricesController : ControllerBase
    {
        IPricesService PricesService;

        public PricesController(IPricesService Pricesservice) 
        { 
            PricesService = Pricesservice;
        }

        [HttpPost]
        public void RegistryPricesPerCategory(CategoriesPrices Prices)
        {
            PricesService.RegistryPricesPerCategory(Prices);
        }
    }
}
