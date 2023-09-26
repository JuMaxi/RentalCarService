using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class CountriesService : ICountriesService
    {
        IValidateCountries ValidateCountries;
        private readonly ICountriesDBAccess _countriesDBAccess;

        public CountriesService(IValidateCountries validateCountries, ICountriesDBAccess countriesDBAccess)
        {
            ValidateCountries = validateCountries;
            _countriesDBAccess = countriesDBAccess;
        }

        public void InsertNewCountry(Countries country)
        {
            ValidateCountries.ValidateNameCountry(country.Country);

            _countriesDBAccess.AddNewCountry(country);
        }

        public List<Countries> ReadCountriesDB()
        {
            return _countriesDBAccess.GetCountries();
        }

        public void DeleteCountry(int Id)
        {
           _countriesDBAccess.DeleteCountry(Id);
        }

        public void UpdateCountry(Countries country)
        {
            ValidateCountries.ValidateNameCountry(country.Country);

            Countries toUpdate = _countriesDBAccess.GetCountryById(country.Id);
            toUpdate.Country = country.Country;

            _countriesDBAccess.UpdateCountry(toUpdate);
        }
    }
}
