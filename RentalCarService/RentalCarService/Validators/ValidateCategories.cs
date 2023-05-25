using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidateCategories : IValidateCategories
    {
        public void ValidateCategory(Categories Category)
        {
            if(Category.Code == null
                || Category.Code.Length == 0)
            {
                throw new Exception("The Code of de Car must be filled to continue.");
            }
            else
            {
                if(Category.Description == null
                    || Category.Description.Length == 0)
                {
                    throw new Exception("The Description of the car must be filled to continue.");
                }
            }
        }
    }
}
