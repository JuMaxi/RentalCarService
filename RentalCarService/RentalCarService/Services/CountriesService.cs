using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class CountriesService : ICountriesService
    {
        IValidateCountries ValidateCountries;
        private readonly RentalCarsDBContext _dbContext;

        public CountriesService(IValidateCountries validateCountries, RentalCarsDBContext dbContext)
        {
            ValidateCountries = validateCountries;
            _dbContext = dbContext;
        }

        public void InsertNewCountry(Countries Countries)
        {
            ValidateCountries.ValidateNameCountry(Countries.Country);
            _dbContext.Countries.Add(Countries);
            _dbContext.SaveChanges();
        }

        public List<Countries> ReadCountriesDB()
        {
            var allCountries = _dbContext.Countries.ToList();
            return allCountries;
        }

        public void DeleteCountry(int Id)
        {
            Countries toRemove = _dbContext.Countries.Find(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateCountry(Countries Countries)
        {
            ValidateCountries.ValidateNameCountry(Countries.Country);
            Countries toUpdate = _dbContext.Countries.Find(Countries.Id);
            toUpdate.Country = Countries.Country;
            _dbContext.SaveChanges();
        }
    }
}
