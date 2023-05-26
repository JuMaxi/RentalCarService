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

        [HttpPut]
        public void UpdatePricesPerCategory(CategoriesPrices Prices)
        {
            PricesService.UpdatePricePerCategory(Prices);
        }

        [HttpDelete] 
        public void DeletePricesPerCategory([FromQuery] string CodeCategory, int Id)
        {
            if(Id == 0)
            {
                PricesService.DeletePricePerCategoryFullCategory(CodeCategory);
            }
            else
            {
                PricesService.DeletePricePerCategoryId(Id);
            }
        }
    }
}
