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
            DateTime newDate = candidate.StartDay;
            int index = 0;
            int numberBookings = 1;
            
            while(index < nearbyBookings.Count)
            {
                if(newDate == nearbyBookings[index].StartDay)
                {
                    //add one hour to give time to clean the car.
                    if (candidate.HourReturnCar.AddHours(1) >= nearbyBookings[index].HourGetCar)
                    {
                        numberBookings++;
                        break;
                    }
                }
                if (newDate == candidate.ReturnDay)
                {
                    newDate = candidate.StartDay.AddDays(-1);
                    index++;
                }
                newDate = newDate.AddDays(1);
            }

            if((amountCarsInCategory / numberBookings) < 1)
            {
                return false;
            }
            return true;
        }
    }
}
