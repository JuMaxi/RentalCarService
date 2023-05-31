using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Validators
{
    public class ValidateCategories : IValidateCategories
    {
        IAccessDataBase AccessDB;
        public ValidateCategories(IAccessDataBase Acccess)
        {
            AccessDB = Acccess;
        }

        public void ValidateCategory(Categories Category)
        {
            if (Category.Code == null
                || Category.Code.Length == 0)
            {
                throw new Exception("The Code of de Car must be filled to continue.");
            }
            ValidateUniqueCodeCategory(Category);
            ValidateDescriptionCategory(Category);
        }
        private void ValidateUniqueCodeCategory(Categories Category)
        {
            string Select = "select * from Categories where Code='" + Category.Code + "'";

            IDataReader Reader = AccessDB.AccessReader(Select);

            while (Reader.Read())
            {
                throw new Exception("The Code Category must be unique. Change the code to continue.");
            }
        }
        private void ValidateDescriptionCategory(Categories Category)
        {
            if (Category.Description == null
                    || Category.Description.Length == 0)
            {
                throw new Exception("The Description of the car must be filled to continue.");
            }
        }
    }
}
