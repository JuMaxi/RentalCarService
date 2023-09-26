using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class ExtraDBAccess : IExtraDBAccess
    {
        private readonly RentalCarsDBContext _dbContext;
        public ExtraDBAccess(RentalCarsDBContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        public List<Extraa> GetExtraDB(Booking booking)
        {
            var ids = booking.BookExtra.Select(bookExtra => bookExtra.Extra.Id).ToList(); // [1, 4]
            var listExtras = _dbContext.Extras.Where(extra => ids.Contains(extra.Id)).ToList(); // usa a lista para filtro
            return listExtras;
        }
    }
}
