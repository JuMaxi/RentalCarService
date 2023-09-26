using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IExtraDBAccess
    {
        public List<Extraa> GetExtraDB(Booking booking);
    }
}
