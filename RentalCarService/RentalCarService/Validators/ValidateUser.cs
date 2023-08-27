using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;

namespace RentalCarService.Validators
{
    public class ValidateUser : IValidateUser
    {
        public void Validate(User User)
        {
            if (User.Name is null || User.Name.Length == 0)
            {
                throw new Exception("The name must be filled and can't be null.");
            }
            if (User.Phone is null || User.Phone.Length == 0)
            {
                throw new Exception("The Phone must be filled and can't be null.");
            }
            if (User.IdentityDocument is null || User.IdentityDocument.Length == 0)
            {
                throw new Exception("The Identity Document must be filled and can't be null.");
            }
            if (User.Birthday.Year >= DateTime.Now.Year)
            {
                throw new Exception("The year can't be greater or equal the actual year.");
            }
            if (User.Nationality.Id == 0)
            {
                throw new Exception("The Nationality Country Id must be filled and can't be null.");
            }
            if (User.Gender is null || User.Gender.Length == 0)
            {
                throw new Exception("The Gender must be filled and can't be null.");
            }
            if (User.CNH is null || User.CNH.Length == 0)
            {
                throw new Exception("The CNH must be filled and can't be null.");
            }
            if (User.CountryCNH.Id == 0)
            {
                throw new Exception("The CNH Country Id must be filled and can't be null.");
            }
            if (User.DateCNH.Year > DateTime.Now.Year || User.DateCNH < User.Birthday.AddYears(18))
            {
                throw new Exception("The Date of the CNH must be less than the current year and greater than the year of user's birthday plus 18.");
            }
            if (User.Password is null || User.Password.Length == 0)
            {
                throw new Exception("The Password must be filled and can't be null.");
            }
            ValidateUserAddress(User.Address);
            ValidateUserEmail(User.Email);

        }
        static private void ValidateUserAddress(UserAddress Address)
        {
            if (Address.Street is null || Address.Street.Length == 0)
            {
                throw new Exception("The Street must be filled and can't be null.");
            }
            if (Address.Number is null || Address.Number.Length == 0)
            {
                throw new Exception("The Number must be filled and can't be null.");
            }
            if (Address.Neighborhood is null || Address.Neighborhood.Length == 0)
            {
                throw new Exception("The Neighborhood must be filled and can't be null.");
            }
            if (Address.City is null || Address.City.Length == 0)
            {
                throw new Exception("The City must be filled and can't be null.");
            }
            if (Address.State is null || Address.State.Length == 0)
            {
                throw new Exception("The State must be filled and can't be null.");
            }
            if (Address.PostalCode is null || Address.PostalCode.Length == 0)
            {
                throw new Exception("The Post Code must be filled and can't be null.");
            }
            if (Address.Country.Id == 0)
            {
                throw new Exception("The Country Id must be filled and can't be null.");
            }
        }
        static private void ValidateUserEmail(string Email)
        {
            if (Email is null || Email.Length == 0)
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
                        if (found == false)
                        {
                            throw new Exception("The Email must have a .");
                        }
                    }
                }
            }
        }
    }
}
