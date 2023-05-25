using RentalCarService.Interfaces;
using System.Collections.Generic;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Services
{
    public class InsertPriceCategories : IInsertPriceCategories
    {
        ICategoriesService CheckCategories;
        IAccessDataBase AccessDataBase;
        public InsertPriceCategories(ICategoriesService Categories, IAccessDataBase AccessDB)
        {
            CheckCategories = Categories;
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

        
    }
}
