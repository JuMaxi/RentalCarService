using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

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

        [HttpGet]
        public List<Brands> ReadBrandsFromDB()
        {
            List<Brands> Brands = BrandsService.ReadBrandsFromDB();

            return Brands;  
        }

        [HttpDelete("{Id}")]
        public void DeleteBrand([FromRoute] int Id)
        {
            BrandsService.DeleteBrand(Id);
        }

        [HttpPut]
        public void UpdateBrand(Brands Brand)
        {
            BrandsService.UpdateBrand(Brand);
        }
        
    }
}
