using RentalCarService.Models.Requests;
using RentalCarService.Models;
using RentalCarService.Models.Responses;
using System.Collections.Generic;

namespace RentalCarService.Interfaces
{
    public interface IUserMapper
    {
        public User ConvertToUser(UserRequest userRequest);
        public List<UserResponse> ConvertToUserResponse(List<User> users);
    }
}
