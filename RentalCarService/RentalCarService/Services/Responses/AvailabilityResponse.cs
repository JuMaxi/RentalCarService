using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Interfaces.Responses;
using RentalCarService.Models;
using RentalCarService.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services.Responses
{
    public class AvailabilityResponse : IAvailabilityResponse
    {
        private readonly RentalCarsDBContext _dbcontext;

        public AvailabilityResponse(RentalCarsDBContext dbContext)
        {
            _dbcontext = dbContext;
        }

        private List<Booking> AvailabilityFleet(Availability availability)
        {

            List<Booking> nearbyBookings = FindBookFromDB(availability);

            List<Booking> bookedCategories = new List<Booking>();
            foreach (Booking booking in nearbyBookings)
            {
                if (availability.ReturnDay >= booking.StartDay
                && availability.StartDay <= booking.StartDay)
                {
                    //add one hour to give time to clean the car.
                    if ((availability.ReturnDay.Hour + 1) >= booking.StartDay.Hour)
                    {
                        bookedCategories.Add(booking);
                    }
                }
            }
            return bookedCategories;
        }

        public List<Car> CompareAvailability(Availability availability)
        {
            List<Car> fleet = FindCarFromDB(availability.BranchGetCar);

            List<Booking> bookedCategories = AvailabilityFleet(availability);

            foreach(Booking c in bookedCategories)
            {
                foreach(Car carCategory in fleet)
                {
                    if(c.Category.Id == carCategory.Category.Id)
                    {
                        fleet.Remove(carCategory);
                    }
                }
            }

            return fleet;
        }

        private List<Car> FindCarFromDB(int id)
        {
            var fleet = _dbcontext.Fleet
                .Include(c => c.Category)
                .ThenInclude(p => p.PriceBands)
                .Include(br => br.Branch)
                .ThenInclude(o => o.OpeningHours)
                .Where(b => b.Branch.Id == id)
                .ToList();

            return fleet;
        }

        private List<Booking> FindBookFromDB(Availability availability)
        {
            List<Booking> books = _dbcontext.Books
                .Where(d => d.StartDay <= availability.ReturnDay)
                .Where(c => c.StartDay >= availability.StartDay)
                .ToList();

            return books;
        }
        
    }
}
