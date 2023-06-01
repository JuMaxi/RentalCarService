using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;

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

        public List<Brands> ReadBrandsFromDB()
        {
            List<Brands> BrandsList = new List<Brands>();
            string Select = "Select * from Brands";
            
            IDataReader Reader = AccessDB.AccessReader(Select);

            while(Reader.Read())
            {
                Brands Brand = new Brands();
                Brand.Id = Convert.ToInt32(Reader["Id"]);
                Brand.Brand = Reader["Brand"].ToString();

                BrandsList.Add(Brand);
            }
            return BrandsList;
        }
        public void DeleteBrand(int Id)
        {
            string Delete = "delete from Brands where Id=" + Id;
            AccessDB.AccessNonQuery(Delete);
        }

        public void UpdateBrand(Brands Brand)
        {
            string Update = "Update Brands set Brand='" + Brand.Brand + "' where Id=" + Brand.Id;
            AccessDB.AccessNonQuery(Update);
        }
    }
}
