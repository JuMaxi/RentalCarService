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
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public void InsertNewUserDB(UserRequest userRequest)
        {
            User user = ConvertToUser(userRequest);
            _userService.InserNewUser(user);
        }

        [HttpGet]
        public List<UserResponse> ReadUsersFromDB()
        {
            List<User> Users = _userService.ReadUsersFromDB();
            List<UserResponse> usersResponse = ConvertToUserResponse(Users);
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
            User user= ConvertToUser(userRequest);

            user.Id= id;

            _userService.UpdateUser(user);
        }
        private User ConvertToUser(UserRequest userRequest)
        {
            User user = new User();
            user.Name= userRequest.Name;
            user.Phone= userRequest.Phone;
            user.IdentityDocument = userRequest.IdentityDocument;
            
            UserAddress address = new UserAddress();
            address.Street = userRequest.Address.Street;
            address.Number = userRequest.Address.Number;
            address.Neighborhood= userRequest.Address.Neighborhood;
            address.City= userRequest.Address.City;
            address.State= userRequest.Address.State;
            address.PostalCode= userRequest.Address.PostalCode;
            
            Countries country = new Countries();
            country.Id = userRequest.Address.Country;
            address.Country = country;

            user.Address= address;
            user.Birthday = userRequest.Birthday;
            country.Id = userRequest.Nationality;
            user.Nationality = country;
            user.Gender= userRequest.Gender;
            user.CNH = userRequest.DriverLicense.Number;
            country.Id = userRequest.DriverLicense.IssuingCountry;
            user.CountryCNH= country;
            user.DateCNH= userRequest.DriverLicense.IssuingDate;
            user.Email= userRequest.Email;
            user.Password= userRequest.Password;
            
            return user;
        }
       
        private List<UserResponse> ConvertToUserResponse(List<User> users)
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

                DrivingLicenseResponse drivingLicense = new DrivingLicenseResponse();

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
