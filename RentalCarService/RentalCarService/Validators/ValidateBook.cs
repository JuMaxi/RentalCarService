using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.ComponentModel;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateBook : IValidateBook
    {
        private readonly RentalCarsDBContext _dbcontext;
        public ValidateBook(RentalCarsDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Validate(Book book) 
        {
            if(book.User.Id == 0)
            {
                throw new Exception("The User Id must be filled to continue.");
            }
            ValidateCategory(book.Category);
            ValidateHoursBranchGetCar(book);
            ValidateHoursBranchReturnCar(book);
            
        }
        private void ValidateCategory(Categories category)
        {
            if (category.Id == 0)
            {
                throw new Exception("The Category Id must be filled to continue.");
            }
            FindCarCategoryFromDB(category.Id);
            
        }
        private void FindCarCategoryFromDB(int id)
        {
            var car = _dbcontext.Fleet.Include(c => c.Category).Where(f => f.Category.Id== id).FirstOrDefault();

            if (car == null)
            {
                throw new Exception("There is no cars available in this category.");
            }
        }

        private Branchs FindBranchFromDB(int id)
        {
            var branch = _dbcontext.Branches.Include(O => O.OpeningHours).Where(B => B.Id == id).FirstOrDefault();
            return branch;
        }

        private void ValidateHoursBranchGetCar(Book book)
        {
            if(book.BranchGet.Id == 0)
            {
                throw new Exception("The Branch to get the car must be chosen to continue.");
            }

            Branchs branchGet = FindBranchFromDB(book.BranchGet.Id);

            foreach(OpeningHours h in branchGet.OpeningHours)
            {
                if(book.StartDay.DayOfWeek == h.DayOfWeek)
                {
                    if(book.HourGetCar < h.Opens)
                    {
                        throw new Exception("This branch opens at " + h.Opens + " you can't get the car before this time.");
                    }
                }
            }
        }
        private void ValidateHoursBranchReturnCar(Book book)
        {
            if(book.BranchReturn.Id == 0)
            {
                throw new Exception("The Branch to return the car must be chosen to continue.");
            }
            Branchs branchReturn = FindBranchFromDB(book.BranchReturn.Id);

            foreach(OpeningHours h in branchReturn.OpeningHours)
            {
                if(book.ReturnDay.DayOfWeek == h.DayOfWeek)
                {
                    if(book.HourReturnCar > h.Closes)
                    {
                        throw new Exception("This branch closes at " + h.Closes + " you can't return the car after this time.");
                    }
                }
            }
        }
        
    }
}
