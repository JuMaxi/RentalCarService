using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class CountriesService : ICountriesService
    {
        IAccessDataBase AccessDB;
        IValidateCountries ValidateCountries;

        public CountriesService(IAccessDataBase accessDB, IValidateCountries validateCountries)
        {
            AccessDB = accessDB;
            ValidateCountries = validateCountries;
        }

        public void InsertNewCountry(Countries Countries)
        {
            ValidateCountries.ValidateNameCountry(Countries.Country);

            string Insert = "insert into Countries (Country) values ('" + Countries.Country + "'";

            AccessDB.AccessNonQuery(Insert);
        }
    }
}
