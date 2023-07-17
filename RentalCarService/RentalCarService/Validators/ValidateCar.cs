using Microsoft.AspNetCore.Razor.TagHelpers;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidateCar : IValidateCar
    {
        public void Validate(Car Car)
        {
            if (Car.Model.Length == 0 || Car.Model == null)
            {
                throw new Exception("The Model Car must be filled to continue and can't be null.");
            }

            if (Car.Year.Year > DateTime.Now.Year)
            {
                throw new Exception("The Year must less or equal the current year.");
            }
            if (Car.Transmission.Length == 0 || Car.Transmission == null)
            {
                throw new Exception("The Transmission Car must be filled to continue and can't be null.");
            }
            if(Car.Doors <= 0)
            {
                throw new Exception("The Doors Car must be greater than zero.");
            }
            if(Car.Seats <= 0)
            {
                throw new Exception("The Seats Car must be greater than zero.");
            }
            if(Car.AirConditioner.Length == 0 || Car.AirConditioner== null)
            {
                throw new Exception("The AirConditioner must be filled to continue and can't be null");
            }
            if(Car.TrunkSize <= 0)
            {
                throw new Exception("The Trunk Size must be greater than zero.");
            }
            if(Car.NumberPlate.Length== 0 || Car.NumberPlate == null)
            {
                throw new Exception("The Number Plate Car must be filled to continue and can't be null.");
            }
        }
    }
}
