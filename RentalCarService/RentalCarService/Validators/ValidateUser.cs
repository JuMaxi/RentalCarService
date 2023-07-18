using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidateUser : IValidateUser
    {
        public void Validate(User User) 
        { 
            if(User.Name.Length == 0 || User.Name == null)
            {
                throw new Exception("The name must be filled and can't be null.");
            }
            if(User.Phone.Length == 0 || User.Phone == null)
            {
                throw new Exception("The Phone must be filled and can't be null.");
            }
            if(User.IdentityDocument.Length == 0 || User.IdentityDocument== null)
            {
                throw new Exception("The Identity Document must be filled and can't be null.");
            }
            if(User.Birthday.Year >= DateTime.Now.Year)
            {
                throw new Exception("The year can't be greater or equal the actual year.");
            }
            if(User.Nationality.Id == 0)
            {
                throw new Exception("The Nationality Country Id must be filled and can't be null.");
            }
            if (User.Gender.Length == 0 || User.Gender == null)
            {
                throw new Exception("The Gender must be filled and can't be null.");
            }
            if(User.CNH.Length == 00 || User.CNH == null)
            {
                throw new Exception("The CNH must be filled and can't be null.");
            }
            if(User.CountryCNH.Id == 0)
            {
                throw new Exception("The CNH Country Id must be filled and can't be null.");
            }
            if(User.DateCNH.Year >= DateTime.Now.Year || User.DateCNH.Year < User.Birthday.Year)
            { 
                throw new Exception("The Date of the CNH must be less than the current year and greater than the year of user's birthday");
            }
            if (User.Password.Length == 0 || User.Password == null)
            {
                throw new Exception("The Password must be filled and can't be null.");
            }
            ValidateUserEmail(User.Email);
            ValidateUserAddress(User.Address);
        }
        private void ValidateUserAddress(UserAddress Address)
        {
            if(Address.Street.Length == 0 || Address.Street== null)
            {
                throw new Exception("The Street must be filled and can't be null.");
            }
            if(Address.Number.Length == 0 || Address.Number == null)
            {
                throw new Exception("The Number must be filled and can't be null.");
            }
            if(Address.Neighborhood.Length == 0 || Address.Neighborhood == null)
            {
                throw new Exception("The Neighborhood must be filled and can't be null.");
            }
            if (Address.City.Length == 0 || Address.City == null)
            {
                throw new Exception("The City must be filled and can't be null.");
            }
            if(Address.State.Length == 0 || Address.State == null)
            {
                throw new Exception("The State must be filled and can't be null.");
            }
            if(Address.PostalCode.Length == 0 || Address.PostalCode == null)
            {
                throw new Exception("The Post Code must be filled and can't be null.");
            }
            if(Address.Country.Id == 0)
            {
                throw new Exception("The Country Id must be filled and can't be null.");
            }
        }
        private void ValidateUserEmail(string Email)
        {
            if (Email.Length == 0 || Email == null)
            {
                throw new Exception("The Email must be filled and can't be null.");
            }
            else
            {
                if (Email.IndexOf("@") < 0)
                {
                    throw new Exception("The Email must have a @");
                }
                else
                {
                    int Character = Email.IndexOf("@");

                    if (Character + 1 == Email.Length)
                    {
                        throw new Exception("The Email must have characters after the @");
                    }
                    else
                    {
                        bool found = false;
                        for (int Position = Character + 1; Position < Email.Length; Position++)
                        {
                            if (Email[Position] == '.')
                            {
                                found = true;
                                break;
                            }
                        }
                        if(found == false)
                        {
                            throw new Exception("The Email must have a .");
                        }
                    }
                }
            }
        }
    }
}
