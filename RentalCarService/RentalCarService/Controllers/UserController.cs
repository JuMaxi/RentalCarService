using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        IUserService UserService;
        public UserController(IUserService userService)
        { 
            UserService= userService;
        }

        [HttpPost]
        public void InsertNewUserDB(User User)
        {
            UserService.InserNewUser(User);
        }

        [HttpGet]
        public List<User> ReadUsersFromDB()
        {
            List<User> Users = UserService.ReadUsersFromDB();
            return Users;
        }
    }
}
