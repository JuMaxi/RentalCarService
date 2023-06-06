using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RentalCarService.Validators
{
    public class ValidateBranchs : IValidateBranchs
    {
        public void ValidateBranch(Branchs Branch)
        {
            if(Branch.Name == null || Branch.Name.Length == 0) 
            {
                throw new Exception("The field Name must be filled to continue and can't be null.");
            }
            if(Branch.Phone == null || Branch.Phone.Length == 0)
            {
                throw new Exception("The field Phone must be filled to continue and can't be null.");
            }
            if(Branch.CountryId == 0)
            {
                throw new Exception("The field CountryId must be filled with a valid Country Id to continue");
            }
            if(Branch.Address == null || Branch.Address.Length == 0)
            {
                throw new Exception("The field Address must be filled to continue and can't be null.");
            }
        }
       
    }
}
