using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriesController : ControllerBase
    {
        ICategoriesService InsertCategories;
        public CategoriesController(ICategoriesService InsertC) 
        { 
            InsertCategories= InsertC;
        }

        [HttpPost]
        public void RegistryNewCategorie(Categories NewCategorie) 
        {
            InsertCategories.RegistryNewCategory(NewCategorie);
        }

        [HttpGet]
        public List<Categories> GetCategories()
        {
            List<Categories> CategoriesCar = InsertCategories.ReadCategoriesFromDB();
            return CategoriesCar;
        }

    }
}
