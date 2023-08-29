using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;

namespace RentalCarService.Validators
{
    public class ValidateBooking : IValidateBooking
    {
        private readonly IBookingDBAccess _bookingDBAccess;
        private readonly IAvailabilityService _availabilityService;
        private readonly ICarDBAccess _carDBAccess;
        private readonly IBranchesDBAccess _branchesDBAccess;
        private readonly IUserDBAccess _userDBAccess;

        public ValidateBooking(IBookingDBAccess bookingDBAccess, IAvailabilityService availabilityService,
            ICarDBAccess carDBAccess, IBranchesDBAccess branchesDBAccess, IUserDBAccess userDBAccess)
        {
            _bookingDBAccess = bookingDBAccess;
            _availabilityService = availabilityService;
            _carDBAccess = carDBAccess;
            _branchesDBAccess = branchesDBAccess;
            _userDBAccess = userDBAccess;
        }
        public void Validate(Booking booking)
        {
            ValidateAvailability(booking);
            ValidateUser(booking.User);
            ValidateCategory(booking.Category);
            ValidateHoursBranchGetCar(booking);
            ValidateHoursBranchReturnCar(booking);

        }
        private void ValidateAvailability(Booking booking)
        {
            List<Booking> nearbyBookings = _bookingDBAccess.GetListBookingByCategoryIdAndDatesStartReturn(booking);
            int countFleetCategory = _carDBAccess.GetCountFleetByCategoryId(booking.Category.Id);

            if (_availabilityService.ExistsAvailabilityForBooking(booking, nearbyBookings, countFleetCategory) == false)
            {
                throw new Exception("There is no availiability for the requested dates");
            }
        }
        private void ValidateUser(User user)
        {
            if (user.Id == 0)
            {
                throw new Exception("The User Id must be filled to continue.");
            }
            if (_userDBAccess.GetUserById(user.Id) == null)
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
            var car = _carDBAccess.GetCarByCategoryId(id);

            if (car == null)
            {
                throw new Exception("There is no cars available in this category.");
            }
        }

        private void ValidateHoursBranchGetCar(Booking booking)
        {
            if (booking.BranchGet.Id == 0)
            {
                throw new Exception("The Branch to get the car must be chosen to continue.");
            }

            bool close = true;
            Branchs branchGet = _branchesDBAccess.GetBranchById(booking.BranchGet.Id);

            foreach (OpeningHours h in branchGet.OpeningHours)
            {
                if (booking.StartDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (booking.StartDay.Hour < h.Opens.Hour)
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
            Branchs branchReturn = _branchesDBAccess.GetBranchById(booking.BranchReturn.Id);

            foreach (OpeningHours h in branchReturn.OpeningHours)
            {
                if (booking.ReturnDay.DayOfWeek == h.DayOfWeek)
                {
                    close = false;

                    if (booking.ReturnDay.Hour > h.Closes.Hour)
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
    }
}
