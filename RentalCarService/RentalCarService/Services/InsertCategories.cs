using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentalCarService.Services
{
    public class InsertCategories : IInsertCategories
    {
        IAccessDataBase AccessDB;

        public InsertCategories(IAccessDataBase accessDB)
        {
            AccessDB = accessDB;
        }
        public void RegistryNewCategory(Categories NewCategory)
        {
            string Insert = "insert into Categories (Code, Description) values ('" + NewCategory.Code + "','" + NewCategory.Description + "')";

            AccessDB.AccessNonQuery(Insert);
        }

        public List<Categories> ReadCategoriesFromDB()
        {
            string Select = "select * from Categories";
            List<Categories> CategoriesCar = new List<Categories>();

            IDataReader Reader = AccessDB.AccessReader(Select);

            while (Reader.Read())
            {
                Categories Categories = new Categories();
                Categories.Id = Convert.ToInt32(Reader["Id"]);
                Categories.Code = Reader["Code"].ToString();
                Categories.Description = Reader["Description"].ToString();
                CategoriesCar.Add(Categories);
            }
            return CategoriesCar;
        }
    }
}
