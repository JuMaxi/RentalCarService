using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Data;

namespace RentalCarService.Services
{
    public class BranchsService : IBranchsService
    {
        IAccessDataBase AccessDB;
        public BranchsService(IAccessDataBase Acccess)
        {
            AccessDB = Acccess;
        }

        public void InsertNewBranch(Branchs Branch)
        {
            string Insert = "insert into Branchs (Name, Phone, CountryId, Address) values ('" +
                Branch.Name + "','" + Branch.Phone + "', " + Branch.CountryId + ",'" + Branch.Address + "')";

            AccessDB.AccessNonQuery(Insert);

            InsertOpeningHours(Branch.OpeningHours);
        }

        private int ReturnLastIdBranch()
        {
            int Id = 0;
            string Select = "select Max(Id) as Last from Branchs";

            IDataReader Reader = AccessDB.AccessReader(Select);

            while (Reader.Read())
            {
                Id = Convert.ToInt32(Reader["Last"]);
            }
            return Id;
        }
        private void InsertOpeningHours(OpeningHours Hours)
        {
            int BranchId = ReturnLastIdBranch();

            string Insert = "insert into OpeningHours (BranchId, MondayToFriday, Saturday, Sunday) values (" +
                BranchId + ",'" + Hours.MondayToFriday + "','" + Hours.Saturday + "','" + Hours.Sunday + "')";

            AccessDB.AccessNonQuery(Insert);

        }
    }
}
