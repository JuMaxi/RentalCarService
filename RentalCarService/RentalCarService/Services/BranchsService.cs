using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RentalCarService.Services
{
    public class BranchsService : IBranchsService
    {
        IValidateBranchs ValidateBranchs;
        private readonly RentalCarsDBContext _dbContext;
        public BranchsService(IValidateBranchs validateBranchs, RentalCarsDBContext dbContext)
        {
            ValidateBranchs = validateBranchs;
            _dbContext = dbContext;
        }

        public void InsertNewBranch(Branchs Branch)
        {
            ValidateBranchs.ValidateBranch(Branch);
            Branch.Country = FindCountryIdDB(Branch.Country.Id);

            _dbContext.Branches.Add(Branch);
            _dbContext.SaveChanges();
        }
        private Countries FindCountryIdDB(int Id)
        {
            Countries Country = _dbContext.Countries.Where(I => I.Id == Id).FirstOrDefault();
            return Country;
        }

        public List<Branchs> ReadBranchesFromDB()
        {
            var allBranches = _dbContext.Branches.Include(b => b.OpeningHours).ToList();
            return allBranches;
        }

        public void DeleteBranch(int Id)
        {
            Branchs toRemove = FindOpeningHoursDB(Id);

            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        private Branchs FindOpeningHoursDB(int BranchsId)
        {
            Branchs Branch = _dbContext.Branches.Include(b => b.OpeningHours).Where(B => B.Id == BranchsId).FirstOrDefault();
            foreach (OpeningHours Hours in Branch.OpeningHours)
                _dbContext.Remove(Hours);
            return Branch;
        }
        public void UpdateBranch(Branchs Branch) 
        {
            Branchs toUpdate = FindOpeningHoursDB(Branch.Id);
            toUpdate.Name = Branch.Name;
            toUpdate.Phone = Branch.Phone;
            toUpdate.Country = FindCountryIdDB(Branch.Country.Id);
            toUpdate.Address = Branch.Address;
            toUpdate.OpeningHours = Branch.OpeningHours;
            
            _dbContext.SaveChanges();
        }
    }
}
