using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RentalCarService.Services
{
    public class BranchsService : IBranchsService
    {
        IAccessDataBase AccessDB;
        IValidateBranchs ValidateBranchs;
        public BranchsService(IAccessDataBase Acccess, IValidateBranchs validateBranchs)
        {
            AccessDB = Acccess;
            ValidateBranchs = validateBranchs;
        }

        public void InsertNewBranch(Branchs Branch)
        {
            ValidateBranchs.ValidateBranch(Branch);

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
        private void InsertOpeningHours(List<OpeningHours> OpeningHours)
        {
            int BranchId = ReturnLastIdBranch();

            foreach (OpeningHours Hour in OpeningHours)
            {
                string Insert = "insert into OpeningHours (BranchId, DayOfWeek, Opens, Closes) values (" +
                BranchId + ",'" + Hour.DayOfWeek + "','" + Hour.Opens + "','" + Hour.Closes + "')";

                AccessDB.AccessNonQuery(Insert);

            }
        }

        public List<Branchs> ReadBranchsFromDB()
        {
            List<Branchs> ListBranchs = new List<Branchs>();

            string Select = "select Branchs.Id, Branchs.Name, Branchs.Phone, Branchs.CountryId, Branchs.Address, " +
                "Countries.Country, " +
                "OpeningHours.BranchId, OpeningHours.DayOfWeek, OpeningHours.Opens, OpeningHours.Closes " +
                "from Branchs " +
                "Inner Join Countries ON Branchs.CountryId = Countries.Id " +
                "Inner Join OpeningHours On Branchs.Id = OpeningHours.BranchId";

            IDataReader Reader = AccessDB.AccessReader(Select);
            int Id = 0;

            while (Reader.Read())
            {
                if (Id != Convert.ToInt32(Reader["Id"]))
                {
                    Branchs Branch = new Branchs();
                    Branch.Id = Convert.ToInt32(Reader["Id"]);
                    Branch.Name = Reader["Name"].ToString();
                    Branch.Phone = Reader["Phone"].ToString();
                    Branch.Address = Reader["Address"].ToString();
                    Branch.CountryId = Convert.ToInt32(Reader["CountryId"]);

                    Countries Country = new Countries();
                    Country.Country = Reader["Country"].ToString();

                    OpeningHours Hours = new OpeningHours();
                    Hours.DayOfWeek = ConvertStringToDayOfWeek((Reader["DayOfWeek"]).ToString());
                    Hours.Opens = TimeOnly.Parse(Reader["Opens"].ToString());
                    Hours.Closes = TimeOnly.Parse(Reader["Closes"].ToString());

                    List<OpeningHours> ListHours = new List<OpeningHours>();
                    ListHours.Add(Hours);
                    Branch.OpeningHours = ListHours;

                    ListBranchs.Add(Branch);

                    Id = Branch.Id;
                }
                else
                {
                    OpeningHours Hours = new OpeningHours();
                    Hours.DayOfWeek = ConvertStringToDayOfWeek((Reader["DayOfWeek"]).ToString());
                    Hours.Opens = TimeOnly.Parse(Reader["Opens"].ToString());
                    Hours.Closes = TimeOnly.Parse(Reader["Closes"].ToString());

                    ListBranchs[ListBranchs.Count - 1].OpeningHours.Add(Hours);
                }
            }

            return ListBranchs;
        }

        private DayOfWeek ConvertStringToDayOfWeek(string day)
        {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
        }
    }
}
