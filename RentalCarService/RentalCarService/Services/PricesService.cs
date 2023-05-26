using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Services
{
    public class PricesService : IPricesService
    {
        IAccessDataBase AccessDataBase;
        IValidatePrices ValidatePrices;
        public PricesService(IAccessDataBase AccessDB, IValidatePrices Validate)
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

        public void RegistryPricesPerCategory(CategoriesPrices Prices)
        {
            ValidatePrices.ValidatePrice(Prices);

            int Id = CheckIdCategory(Prices.CodeCategory);
            string Insert = "insert into CategoryPrices (CategoryId, MinDays, MaxDays, Price) values (" + Id +
                ",'" + Prices.MinDays + "','" + Prices.MaxDays + "'," + Prices.Price + ")";

            AccessDataBase.AccessNonQuery(Insert);
        }

        public void DeletePricePerCategoryId(int Id)
        {
            string Delete = "delete from CategoryPrices where Id=" + Id;
            AccessDataBase.AccessNonQuery(Delete);
        }
        public void DeletePricePerCategoryFullCategory(string CodeCategory)
        {
            int Id = CheckIdCategory(CodeCategory);
            string Delete = "delete from CategoryPrices where CategoryId=" + Id;

            AccessDataBase.AccessNonQuery(Delete);
        }
   
        public void UpdatePricePerCategory(CategoriesPrices Prices)
        {
            int Id = CheckIdCategory(Prices.CodeCategory);
            string Update = "Update CategoryPrices set CategoryId=" + Id + ", MinDays='" + Prices.MinDays + "', MaxDays='" + Prices.MaxDays + "', Price=" + Prices.Price + " where Id=" + Prices.Id;

            AccessDataBase.AccessNonQuery(Update);
        }


    }
}
