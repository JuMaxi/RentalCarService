﻿using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IValidatePrices
    {
       public void ValidatePrice(Categories Prices);
    }
}
