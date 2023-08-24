using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;
        readonly IUserMapper _userMapper;
        public UserController(IUserService userService, IUserMapper userMapper)
        {
            _userService = userService;
            _userMapper = userMapper;
        }

        [HttpPost]
        public void InsertNewUserDB(UserRequest userRequest)
        {
            User user = _userMapper.ConvertToUser(userRequest);
            _userService.InserNewUser(user);
        }

        [HttpGet]
        public List<UserResponse> ReadUsersFromDB()
        {
            List<User> Users = _userService.ReadUsersFromDB();
            List<UserResponse> usersResponse = _userMapper.ConvertToUserResponse(Users);
            return usersResponse;
        }

        [HttpDelete]
        public void DeleteUsersFromDB([FromQuery] int Id)
        {
            _userService.DeleteUser(Id);
        }

        [HttpPut("{id}")]
        public void UpdateUser(UserRequest userRequest, int id)
        {
            User user= _userMapper.ConvertToUser(userRequest);

            user.Id= id;

            _userService.UpdateUser(user);
        }
        

    }
}
