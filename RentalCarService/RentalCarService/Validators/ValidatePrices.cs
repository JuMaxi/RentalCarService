using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidatePrices : IValidatePrices
    {
        public void ValidatePrice(Categories Prices)
        {
            if (Prices.Code == null
                || Prices.Code.Length == 0)
            {
                throw new Exception("The Code Category must be filled to continue.");
            }
            else
            {
                ValidatePriceBands(Prices);
            }
        }
        private void ValidatePriceBands(Categories PriceBands)
        {
            foreach (PriceBands Prices in PriceBands.PriceBands)
            {
                if (Prices.MinDays == 0
                        || Prices.MinDays < 0)
                {
                    throw new Exception("The Min Days must be filled with value different than zero, null or empty");
                }
                else
                {
                    if (Prices.MaxDays == 0
                    || Prices.MaxDays < 0)
                    {
                        throw new Exception("The Max Days must be filled with value different than zero, null or empty.");
                    }
                    else
                    {
                        if (Prices.Price == 0
                            || Prices.Price < 0)
                        {
                            throw new Exception("The Price must be filled to continue and must be greater than zero");
                        }
                    }
                }
            }
        }
    }
}
