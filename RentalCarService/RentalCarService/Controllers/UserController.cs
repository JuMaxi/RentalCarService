using Microsoft.AspNetCore.Mvc;
using RentalCarService.Interfaces;
using RentalCarService.Models;
using RentalCarService.Models.Responses;
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
            UserService = userService;
        }

        [HttpPost]
        public void InsertNewUserDB(User User)
        {
            UserService.InserNewUser(User);
        }

        [HttpGet]
        public List<UserResponse> ReadUsersFromDB()
        {
            List<User> Users = UserService.ReadUsersFromDB();
            List<UserResponse> usersResponse = Convert(Users);
            return usersResponse;
        }

        [HttpDelete]
        public void DeleteUsersFromDB([FromQuery] int Id)
        {
            UserService.DeleteUser(Id);
        }

        [HttpPut]
        public void UpdateUser(User User)
        {
            UserService.UpdateUser(User);
        }

        private List<UserResponse> Convert(List<User> users)
        {
            List<UserResponse> usersResponse = new List<UserResponse>();

            foreach (User user in users)
            {
                UserResponse u = new UserResponse();

                u.Name= user.Name;
                u.Phone= user.Phone;
                u.IdentityDocument= user.IdentityDocument;

                UserAddressResponse addressResponse = new UserAddressResponse();

                addressResponse.Street= user.Address.Street;
                addressResponse.Number= user.Address.Number;
                addressResponse.Neighborhood= user.Address.Neighborhood;
                addressResponse.City= user.Address.City;
                addressResponse.State= user.Address.State;
                addressResponse.PostalCode= user.Address.PostalCode;
                addressResponse.Country = user.Address.Country.Country;
                u.Address = addressResponse;

                u.Birthday= user.Birthday;
                u.Nationality= user.Nationality.Country;
                u.Gender= user.Gender;

                DrivingLicense drivingLicense = new DrivingLicense();

                drivingLicense.Number= user.CNH;
                drivingLicense.IssuingCountry = user.CountryCNH.Country;
                drivingLicense.IssuingDate = user.DateCNH;
                u.DriverLicense= drivingLicense;

                u.Email= user.Email;
                u.Password= user.Password;

                usersResponse.Add(u);
            }

            return usersResponse;
        }

    }
}
