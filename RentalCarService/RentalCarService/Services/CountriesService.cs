using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

            string Insert = "insert into Countries (Country) values ('" + Countries.Country + "')";

            AccessDB.AccessNonQuery(Insert);
        }

        public List<Countries> ReadCountriesDB()
        {
            List<Countries> Countries = new List<Countries>();
            string Select = "select * from Countries";
            IDataReader Reader = AccessDB.AccessReader(Select);

            while(Reader.Read())
            {
                Countries Country = new Countries();
                Country.Id = Convert.ToInt32(Reader["Id"]);
                Country.Country = Reader["Country"].ToString();

                Countries.Add(Country);
            }
            return Countries;
        }
    }
}
