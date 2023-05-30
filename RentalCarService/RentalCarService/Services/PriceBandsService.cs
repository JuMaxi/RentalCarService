using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace RentalCarService.Services
{
    public class PriceBandsService : IPriceBandsService
    {
        IAccessDataBase AccessDataBase;
        IValidatePrices ValidatePrices;
        public PriceBandsService(IAccessDataBase AccessDB, IValidatePrices Validate)
        {
            AccessDataBase = AccessDB;
            ValidatePrices = Validate;
        }

        private int CheckIdCategory(string CodeCategory)
        {
            string Select = "select * from Categories where Code= '" + CodeCategory + "'";

            IDataReader Reader = AccessDataBase.AccessReader(Select);

            while (Reader.Read())
            {
                int Id = Convert.ToInt32(Reader["Id"]);
                return Id;
            }

            return 0;
        }

        public void RegistryPricesPerCategory(Categories PriceBands)
        {
            ValidatePrices.ValidatePrice(PriceBands);

            int Id = CheckIdCategory(PriceBands.Code);
            foreach (PriceBands Price in PriceBands.PriceBands)
            {
                string Insert = "insert into PriceBands (CategoryId, MinDays, MaxDays, Price) values (" + Id +
                ",'" + Price.MinDays + "','" + Price.MaxDays + "'," + Price.Price + ")";

                AccessDataBase.AccessNonQuery(Insert);
            }
        }

        public void DeletePricePerCategoryId(int Id)
        {
            string Delete = "delete from PriceBands where Id=" + Id;
            AccessDataBase.AccessNonQuery(Delete);
        }
        public void DeletePricePerCategoryFullCategory(string CodeCategory)
        {
            int Id = CheckIdCategory(CodeCategory);
            string Delete = "delete from PriceBands where CategoryId=" + Id;

            AccessDataBase.AccessNonQuery(Delete);
        }

        public void UpdatePricePerCategory(Categories Prices)
        {
            int Id = CheckIdCategory(Prices.Code);
            foreach (PriceBands P in Prices.PriceBands)
            {
                string Update = "Update PriceBands set CategoryId=" + Id + ", MinDays='" + P.MinDays + "', MaxDays='" + P.MaxDays + "', Price=" + P.Price
                    + " where Id=" + P.Id;

                AccessDataBase.AccessNonQuery(Update);
            }

        }

        public List<Categories> ReadPriceBandsPerCategoryDB()
        {
            List<Categories> PriceBandsCategory = new List<Categories>();

            string Select = "select Categories.Id, Categories.Code, Categories.Description," +
                " PriceBands.Id as IdPrice, PriceBands.MinDays, PriceBands.MaxDays, PriceBands.Price" +
                " from Categories" +
                " inner join PriceBands ON Categories.Id = PriceBands.CategoryId";

            IDataReader Reader = AccessDataBase.AccessReader(Select);

            int Id = 0;
            while(Reader.Read())
            {
                if(Id != Convert.ToInt32(Reader["Id"]))
                {
                    Categories Categories = new Categories();
                    Categories.Id = Convert.ToInt32(Reader["Id"]);
                    Categories.Code = Reader["Code"].ToString();
                    Categories.Description = Reader["Description"].ToString();

                    PriceBands Prices = new PriceBands();
                    Prices.Id= Convert.ToInt32(Reader["IdPrice"]);
                    Prices.MinDays = Convert.ToInt32(Reader["MinDays"]);
                    Prices.MaxDays = Convert.ToInt32(Reader["MaxDays"]);
                    Prices.Price = Convert.ToInt32(Reader["Price"]);
                    List<PriceBands> List = new List<PriceBands>();
                    List.Add(Prices);
                    Categories.PriceBands = List;

                    PriceBandsCategory.Add(Categories);

                    Id = Convert.ToInt32(Reader["Id"]);
                }
                else
                {
                    PriceBands Prices = new PriceBands();
                    Prices.Id = Convert.ToInt32(Reader["IdPrice"]);
                    Prices.MinDays = Convert.ToInt32(Reader["MinDays"]);
                    Prices.MaxDays = Convert.ToInt32(Reader["MaxDays"]);
                    Prices.Price = Convert.ToInt32(Reader["Price"]);

                    int LastIndex = PriceBandsCategory.Count;

                    PriceBandsCategory[LastIndex-1].PriceBands.Add(Prices);
                }
            }
            return PriceBandsCategory;
        }


    }
}
