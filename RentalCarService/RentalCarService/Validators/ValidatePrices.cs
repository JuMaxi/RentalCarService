using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidatePrices : IValidatePrices
    {
        public void ValidatePrice(CategoriesPrices Prices)
        {
            if (Prices.CodeCategory == null
                || Prices.CodeCategory.Length == 0)
            {
                throw new Exception("The Code Category must be filled to continue.");
            }
            else
            {
                if (Prices.MinDays == null
                    || Prices.MinDays.Length == 0
                    || Prices.MinDays == "0")
                {
                    throw new Exception("The Min Days must be filled with value different than zero, null or empty");
                }
                else
                {
                    if (Prices.MaxDays == null
                    || Prices.MaxDays.Length == 0
                    || Prices.MaxDays == "0")
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
