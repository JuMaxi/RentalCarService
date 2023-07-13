using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface ICountriesService
    {
        public void InsertNewCountry(Countries Countries);
        public List<Countries> ReadCountriesDB();
        public void DeleteCountry(int Id);
        public void UpdateCountry(Countries Countries);
    }
}
