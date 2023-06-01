using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route ("[Controller]")]
    public class BrandsController : ControllerBase
    {
        IBrandsService BrandsService;
        public BrandsController(IBrandsService Brands) 
        { 
            BrandsService= Brands;
        }

        [HttpPost]
        public void InsertNewBrand(Brands Brand)
        {
            BrandsService.InserNewBrand(Brand);
        }
    }
}
