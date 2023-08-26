using RentalCarService.Interfaces;
using System;

namespace RentalCarService.Validators
{
    public class ValidateCountries : IValidateCountries
    {
        public void ValidateNameCountry(string Country)
        {
            if (Country == null
                || Country.Length == 0)
            {
                throw new Exception("The name of country must be filled to continue.");
            }
        }
    }
}
