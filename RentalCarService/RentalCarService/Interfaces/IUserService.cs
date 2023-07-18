using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IUserService
    {
        public void InserNewUser(User User);
        public List<User> ReadUsersFromDB();
        public void DeleteUser(int Id);
        public void UpdateUser(User User);

    }
}
