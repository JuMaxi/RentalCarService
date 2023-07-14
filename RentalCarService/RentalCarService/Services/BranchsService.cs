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
            _dbContext.Branches.Add(Branch);
            _dbContext.SaveChanges();
        }

        public List<Branchs> ReadBranchesFromDB()
        {
            var allBranches = _dbContext.Branches.Include(b => b.OpeningHours).ToList();
            return allBranches;
        }

        private DayOfWeek ConvertStringToDayOfWeek(string day)
        {
            return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
        }

        public void DeleteBranch(int Id)
        {
            Branchs toRemove = _dbContext.Branches.Include(b => b.OpeningHours).Where(B => B.Id == Id).FirstOrDefault();

            foreach(OpeningHours Hours in toRemove.OpeningHours)
                _dbContext.Remove(Hours);

            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }
        public void UpdateBranch(Branchs Branch) 
        {
            
        }
    }
}
