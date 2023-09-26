using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class CountriesDBAccess : ICountriesDBAccess
    {
        readonly RentalCarsDBContext _dbContext;

        public CountriesDBAccess(RentalCarsDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void AddNewCountry(Countries country)
        {
            _dbContext.Countries.Add(country);
            _dbContext.SaveChanges();
        }

        public List<Countries> GetCountries()
        {
            return _dbContext.Countries.ToList();
        }

        public Countries GetCountryById(int id)
        {
            return _dbContext.Countries.Find(id);
        }

        public void DeleteCountry(int Id)
        {
            Countries toRemove = GetCountryById(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateCountry(Countries Countries)
        {
            _dbContext.Countries.Update(Countries);
            _dbContext.SaveChanges();
        }
    }
}
