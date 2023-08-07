using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateBook : IValidateBook
    {
        // TODO: Add validation if the user exists
        private readonly RentalCarsDBContext _dbcontext;
        private readonly IAvailabilityService _availabilityService;

        public ValidateBook(RentalCarsDBContext dbcontext, IAvailabilityService availabilityService)
        {
            _dbcontext = dbcontext;
            _availabilityService = availabilityService;
        }
        public void Validate(Book book)
        {
            List<Book> nearbyBookings = FindBookFromDB(book);

            if (_availabilityService.ExistsAvailabilityForBooking(book, nearbyBookings) == false)
            {
                throw new Exception("There is no availiability for the requested dates");
            }
            if (book.User.Id == 0)
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
            var car = _dbcontext.Fleet.Include(c => c.Category).Where(f => f.Category.Id == id).FirstOrDefault();

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
            if (book.BranchGet.Id == 0)
            {
                throw new Exception("The Branch to get the car must be chosen to continue.");
            }

            bool close = true;
            Branchs branchGet = FindBranchFromDB(book.BranchGet.Id);

            foreach (OpeningHours h in branchGet.OpeningHours)
            {
                if (book.StartDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (book.HourGetCar < h.Opens)
                    {
                        throw new Exception("This branch opens at " + h.Opens + " you can't get the car before this time.");
                    }
                }
            }
            if (close == true)
            {
                throw new Exception("This branch is not open on day " + book.StartDay.DayOfWeek +
                    ". Please select a day that this branch is open.");
            }
        }
        private void ValidateHoursBranchReturnCar(Book book)
        {
            if (book.BranchReturn.Id == 0)
            {
                throw new Exception("The Branch to return the car must be chosen to continue.");
            }

            bool close = true;
            Branchs branchReturn = FindBranchFromDB(book.BranchReturn.Id);

            foreach (OpeningHours h in branchReturn.OpeningHours)
            {
                if (book.ReturnDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (book.HourReturnCar > h.Closes)
                    {
                        throw new Exception("This branch closes at " + h.Closes + " you can't return the car after this time.");
                    }
                }

            }
            if (close == true)
            {
                throw new Exception("This branch is not open on day " + book.ReturnDay.DayOfWeek +
                    ". Please select a day that this branch is open.");
            }
        }

        private List<Book> FindBookFromDB(Book book)
        {
            List<Book> books = _dbcontext.Books
                .Include(c => c.Category)
                .Where(b => b.Category.Id == book.Category.Id)
                .Where(d => d.StartDay <= book.ReturnDay)
                .Where(c => c.StartDay >= book.StartDay)
                .ToList();

            return books;
        }
    }
}
