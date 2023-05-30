using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PricesController : ControllerBase
    {
        IPriceBandsService PricesService;

        public PricesController(IPriceBandsService Pricesservice)
        {
            PricesService = Pricesservice;
        }

        [HttpPost]
        public void RegistryPricesPerCategory(Categories PriceBands)
        {
            PricesService.RegistryPricesPerCategory(PriceBands);
        }

        [HttpPut]
        public void UpdatePricesPerCategory(Categories Prices)
        {
            PricesService.UpdatePricePerCategory(Prices);
        }

        [HttpDelete]
        public void DeletePricesPerCategory([FromQuery] string CodeCategory, int Id)
        {
            if (Id == 0)
            {
                PricesService.DeletePricePerCategoryFullCategory(CodeCategory);
            }
            else
            {
                PricesService.DeletePricePerCategoryId(Id);
            }
        }

        [HttpGet]
        public List<Categories> ReadPricesPerCategoryDB()
        {
            List<Categories> Prices = PricesService.ReadPriceBandsPerCategoryDB();
            return Prices;
        }
    }
}
