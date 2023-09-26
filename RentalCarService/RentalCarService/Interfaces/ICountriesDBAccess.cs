using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICountriesDBAccess
    {
        public void AddNewCountry(Countries country);
        public List<Countries> GetCountries();
        public Countries GetCountryById(int id);
        public void DeleteCountry(int Id);
        public void UpdateCountry(Countries Countries);
    }
}
