using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CountriesController : ControllerBase
    {
        ICountriesService CountriesService;
        public CountriesController(ICountriesService Countries) 
        {
            CountriesService= Countries;
        }

        [HttpPost]
        public void InsertNewCountry(Countries Countries)
        {
            CountriesService.InsertNewCountry(Countries);
        }

        [HttpGet]
        public List<Countries> ReadCountriesDB()
        {
            List<Countries> Countries = CountriesService.ReadCountriesDB();

            return Countries;
        }

        [HttpDelete]
        public void DeleteCountry([FromQuery] int Id)
        {
            CountriesService.DeleteCountry(Id);
        }

        [HttpPut]
        public void UpdateCountry(Countries Countries)
        {
            CountriesService.UpdateCountry(Countries);
        }
    }
}
