using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Linq;

namespace RentalCarService.DbAccess
{
    public class UserDBAccess : IUserDBAccess
    {
        readonly RentalCarsDBContext _dbcontext;

        public UserDBAccess(RentalCarsDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public User GetUserById(int id)
        {
            User user = _dbcontext.Users.Find(id);

            return user;
        }

        public User GetUserByIdThenInclude(int id)
        {
            return _dbcontext.Users
               .Include(c => c.CountryCNH)
               .Include(n => n.Nationality)
               .Include(a => a.Address)
               .ThenInclude(ac => ac.Country)
               .Where(I => I.Id == id)
               .FirstOrDefault();
        }
    }
}
