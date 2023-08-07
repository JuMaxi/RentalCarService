using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidateExtra : IValidateExtra
    {
        public void Validate(Extraa Extra)
        {
            if(Extra.Service.Length == 0 || Extra.Service == null)
            {
                throw new Exception("The service/product can't be empty or null.");
            }
            if(Extra.DayCost < 0 || Extra.FixedCost < 0)
            {
                throw new Exception("The cost must be greater than zero.");
            }
            if(Extra.DayCost == 0 && Extra.FixedCost == 0)
            {
                throw new Exception("At least one field cost must be filled to continue.");
            }
            if(Extra.DayCost > 0 && Extra.FixedCost > 0)
            {
                throw new Exception("Just one field cost must be filled.");
            }
        }

    }
}
