using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;

namespace RentalCarService.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        public bool ExistsAvailabilityForBooking(Booking candidate, List<Booking> nearbyBookings, int amountCarsInCategory = 1)
        {
            foreach (Booking booking in nearbyBookings)
            {
                if (candidate.ReturnDay >= booking.StartDay
                && candidate.StartDay <= booking.StartDay)
                {
                    //add one hour to give time to clean the car.
                    if (candidate.HourReturnCar.AddHours(1) >= booking.HourGetCar)
                    {
                        if(amountCarsInCategory < nearbyBookings.Count)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
