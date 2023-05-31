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
    }
}
