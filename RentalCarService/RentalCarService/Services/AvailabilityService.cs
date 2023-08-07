using RentalCarService.Interfaces;
using RentalCarService.Models;
using System;
using System.Collections.Generic;

namespace RentalCarService.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        public bool ExistsAvailabilityForBooking(Book candidate, List<Book> nearbyBookings)
        {
            foreach (Book b in nearbyBookings)
            {
                DateTime actualBook = candidate.StartDay;

                while (actualBook <= candidate.ReturnDay)
                {
                    if (b.StartDay == actualBook)
                    {
                        return false;
                    }
                    actualBook = actualBook.AddDays(1);
                }
            }

            return true;
        }
    }
}
