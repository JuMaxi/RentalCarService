using RentalCarService.Interfaces;
using System.Collections.Generic;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Services
{
    public class PricesService : IPricesService
    {
        IAccessDataBase AccessDataBase;
        public PricesService(IAccessDataBase AccessDB)
        {
            AccessDataBase = AccessDB;
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
            int Id = CheckIdCategory(Prices.CodeCategory);
            string Insert = "insert into PricesPerCategory (CategoryId, MinDays, MaxDays, Price) values (" + Id +
                ",'" + Prices.MixDays + "','" + Prices.MaxDays + "'," + Prices.Price + ")";

            AccessDataBase.AccessNonQuery(Insert);

        }


    }
}
