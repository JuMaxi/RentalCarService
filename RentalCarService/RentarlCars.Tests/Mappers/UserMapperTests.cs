using FluentAssertions;
using RentalCarService.Mappers;
using RentalCarService.Migrations;
using RentalCarService.Models;
using RentalCarService.Models.Requests;
using RentalCarService.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentarlCars.Tests.Mappers
{
    public class UserMapperTests
    {
        [Fact]
        public void Checking_If_UserRequest_is_Equal_User()
        {
            UserRequest userRequest = new()
            {
                Name = "Joao da Silva",
                Phone = "(44) 7485-4125",
                IdentityDocument = "3652-1425-2133",
                Address = new() 
                {
                    Street = "Beautiful Street",
                    Number = "5412",
                    Neighborhood = "Hapiness",
                    City = "Neverland",
                    State = "Snowstate",
                    PostCode = "A2 K8547",
                    Country = 1
                },
                Birthday = new DateTime(1970, 05, 03),
                Nationality = 2001,
                Gender = "Male",
                DriverLicense = new()
                {
                    Number = "2547.258-741",
                    IssuingCountry = 15,
                    IssuingDate = new DateTime(1990, 06, 25)
                },
                Email = "joaodasilvar@joaodasilva.com",
                Password = "L5-&%o$!Yb4"
            };

            UserMapper userMapper = new();
            User user = userMapper.ConvertToUser(userRequest);

            user.Name.Should().Be(userRequest.Name);
            user.Phone.Should().Be(userRequest.Phone);
            user.IdentityDocument.Should().Be(userRequest.IdentityDocument);
            user.Address.Street.Should().Be(userRequest.Address.Street);
            user.Address.Number.Should().Be(userRequest.Address.Number);
            user.Address.Neighborhood.Should().Be(userRequest.Address.Neighborhood);
            user.Address.City.Should().Be(userRequest.Address.City);
            user.Address.State.Should().Be(userRequest.Address.State);
            user.Address.PostalCode.Should().Be(userRequest.Address.PostCode);
            user.Address.Country.Id.Should().Be(userRequest.Address.Country);
            user.Birthday.Should().Be(userRequest.Birthday);
            user.Nationality.Id.Should().Be(userRequest.Nationality);
            user.Gender.Should().Be(userRequest.Gender);
            user.CNH.Should().Be(userRequest.DriverLicense.Number);
            user.CountryCNH.Id.Should().Be(userRequest.DriverLicense.IssuingCountry);
            user.DateCNH.Should().Be(userRequest.DriverLicense.IssuingDate);
            user.Email.Should().Be(userRequest.Email);
            user.Password.Should().Be(userRequest.Password);
        }

        [Fact]
        public void Checking_If_UserResponse_is_Equal_User()
        {
            List<User> users = new()
            {
                new User
                {
                    Name = "Paula Fox",
                    Phone = "(33) 6352-5478",
                    IdentityDocument = "2536-4125-1122",
                    Address= new()
                    {
                        Street = "Love Street",
                        Number = "41",
                        Neighborhood = "Green Tree",
                        City = "GreatOrange",
                        State = "Green tree of Oranges",
                        PostalCode = "OR 74JA",
                        Country = new() {Country = "Brazil"}
                    },
                    Birthday = new DateTime(1979, 02, 01),
                    Nationality = new() {Country = "Brazil"},
                    Gender = "Female",
                    CNH = "4152-4758.1425",
                    CountryCNH= new() {Country = "Brazil"},
                    DateCNH = new DateTime(1995, 08, 25),
                    Email = "paulafox@paulafox.com",
                    Password = "I0&@3r6Pf!)"
                }
            };

            UserMapper userMapper = new();
            List<UserResponse> usersResponse = userMapper.ConvertToUserResponse(users);

            usersResponse[0].Name.Should().Be(users[0].Name);
            usersResponse[0].Phone.Should().Be(users[0].Phone);
            usersResponse[0].IdentityDocument.Should().Be(users[0].IdentityDocument);
            usersResponse[0].Address.Street.Should().Be(users[0].Address.Street);
            usersResponse[0].Address.Number.Should().Be(users[0].Address.Number);
            usersResponse[0].Address.Neighborhood.Should().Be(users[0].Address.Neighborhood);
            usersResponse[0].Address.City.Should().Be(users[0].Address.City);
            usersResponse[0].Address.State.Should().Be(users[0].Address.State);
            usersResponse[0].Address.PostCode.Should().Be(users[0].Address.PostalCode);
            usersResponse[0].Address.Country.Should().Be(users[0].Address.Country.Country);
            usersResponse[0].Birthday.Should().Be(users[0].Birthday);
            usersResponse[0].Nationality.Should().Be(users[0].Nationality.Country);
            usersResponse[0].Gender.Should().Be(users[0].Gender);
            usersResponse[0].DriverLicense.Number.Should().Be(users[0].CNH);
            usersResponse[0].DriverLicense.IssuingCountry.Should().Be(users[0].CountryCNH.Country);
            usersResponse[0].DriverLicense.IssuingDate.Should().Be(users[0].DateCNH);
            usersResponse[0].Email.Should().Be(users[0].Email);
            usersResponse[0].Password.Should().Be(users[0].Password);

        }
    }
}
