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
        ICategoriesService CategoriesService;
        public CategoriesController(ICategoriesService InsertC) 
        { 
            CategoriesService= InsertC;
        }

        [HttpPost]
        public void RegistryNewCategorie(Categories NewCategorie) 
        {
            CategoriesService.RegistryNewCategory(NewCategorie);
        }

        [HttpGet]
        public List<Categories> GetCategories()
        {
            List<Categories> CategoriesCar = CategoriesService.ReadCategoriesFromDB();
            return CategoriesCar;
        }

        [HttpDelete]
        public void DeleteCategory([FromQuery] int Id)
        {
            CategoriesService.DeleteCategory(Id);
        }

        [HttpPut]
        public void UpdateCategory(Categories Category)
        {
            CategoriesService.UpdateCategory(Category);
        }
    }
}
