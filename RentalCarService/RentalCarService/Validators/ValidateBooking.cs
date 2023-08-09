using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RentalCarService.Validators
{
    public class ValidateBooking : IValidateBooking
    {
        private readonly RentalCarsDBContext _dbcontext;
        private readonly IAvailabilityService _availabilityService;

        public ValidateBooking(RentalCarsDBContext dbcontext, IAvailabilityService availabilityService)
        {
            _dbcontext = dbcontext;
            _availabilityService = availabilityService;
        }
        public void Validate(Booking booking)
        {
            List<Booking> nearbyBookings = FindBookFromDB(booking);
            int countFleetCategory = FindCountFleetPerCategoryFromDB(booking.Category);

            if (_availabilityService.ExistsAvailabilityForBooking(booking, nearbyBookings, countFleetCategory) == false)
            {
                throw new Exception("There is no availiability for the requested dates");
            }

            ValidateUser(booking.User);
            ValidateCategory(booking.Category);
            ValidateHoursBranchGetCar(booking);
            ValidateHoursBranchReturnCar(booking);

        }

        private void ValidateUser(User user)
        {
            if (user.Id == 0)
            {
                throw new Exception("The User Id must be filled to continue.");
            }
            if(FindUserFromDB(user.Id) == null)
            {
                throw new Exception("You must fill the user with a valid Id.");
            }
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

        private void ValidateHoursBranchGetCar(Booking booking)
        {
            if (booking.BranchGet.Id == 0)
            {
                throw new Exception("The Branch to get the car must be chosen to continue.");
            }

            bool close = true;
            Branchs branchGet = FindBranchFromDB(booking.BranchGet.Id);

            foreach (OpeningHours h in branchGet.OpeningHours)
            {
                if (booking.StartDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (booking.HourGetCar < h.Opens)
                    {
                        throw new Exception("This branch opens at " + h.Opens + " you can't get the car before this time.");
                    }
                }
            }
            if (close == true)
            {
                throw new Exception("This branch is not open on day " + booking.StartDay.DayOfWeek +
                    ". Please select a day that this branch is open.");
            }
        }
        private void ValidateHoursBranchReturnCar(Booking booking)
        {
            if (booking.BranchReturn.Id == 0)
            {
                throw new Exception("The Branch to return the car must be chosen to continue.");
            }

            bool close = true;
            Branchs branchReturn = FindBranchFromDB(booking.BranchReturn.Id);

            foreach (OpeningHours h in branchReturn.OpeningHours)
            {
                if (booking.ReturnDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (booking.HourReturnCar > h.Closes)
                    {
                        throw new Exception("This branch closes at " + h.Closes + " you can't return the car after this time.");
                    }
                }

            }
            if (close == true)
            {
                throw new Exception("This branch is not open on day " + booking.ReturnDay.DayOfWeek +
                    ". Please select a day that this branch is open.");
            }
        }

        private User FindUserFromDB(int id)
        {
            User user = _dbcontext.Users.Find(id);

            return user;
        }
        private List<Booking> FindBookFromDB(Booking booking)
        {
            List<Booking> books = _dbcontext.Books
                .Include(c => c.Category)
                .Where(b => b.Category.Id == booking.Category.Id)
                .Where(d => d.StartDay <= booking.ReturnDay)
                .Where(c => c.StartDay >= booking.StartDay)
                .ToList();

            return books;
        }
        private int FindCountFleetPerCategoryFromDB(Categories category)
        {
            int count = _dbcontext.Fleet.Include(c => c.Category)
                .Where(f => f.Category.Id == category.Id)
                .Count();

            return count;
        }
    }
}
