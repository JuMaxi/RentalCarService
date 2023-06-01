using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Validators
{
    public class ValidateBrands : IValidateBrands
    {
        IAccessDataBase AccessDB;
        public ValidateBrands(IAccessDataBase Access)
        {
            AccessDB = Access;
        }

        public void ValidateBrandName(Brands Brand)
        {
            if(Brand.Brand == null || Brand.Brand.Length == 0)
            {
                throw new Exception("The Brand must be filled and can't be null. Fill the field to continue.");
            }
            ValidateRepeatedBrandName(Brand);
        }
        private void ValidateRepeatedBrandName(Brands Brand)
        {
            string Select = "select * from Brands where Brand='" + Brand.Brand + "'";

            IDataReader Reader = AccessDB.AccessReader(Select);

            while (Reader.Read())
            {
                throw new Exception("The Brand " + Brand.Brand + " already exist in this DataBase, insert a different Brand to continue.");
            }
        }
    }
}
