using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class InsertCategoriesController : ControllerBase
    {
        IInsertCategories InsertCategories;
        public InsertCategoriesController(IInsertCategories InsertC) 
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
