using RentalCarService.Interfaces;
using RentalCarService.Models;

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
    }
}
