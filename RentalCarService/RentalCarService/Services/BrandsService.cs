using RentalCarService.Interfaces;
using RentalCarService.Models;

namespace RentalCarService.Services
{
    public class BrandsService : IBrandsService
    {
        IAccessDataBase AccessDB;
        IValidateBrands ValidateBrands;
        public BrandsService(IAccessDataBase accessDB, IValidateBrands validateBrands)
        {
            AccessDB = accessDB;
            ValidateBrands = validateBrands;
        }
        public void InserNewBrand(Brands Brand) 
        {
            ValidateBrands.ValidateBrandName(Brand);

            string Insert = "Insert into Brands (Brand) values ('" + Brand.Brand + "')";

            AccessDB.AccessNonQuery(Insert);
        }

    }
}
