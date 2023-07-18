using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCarService.Interfaces;
using RentalCarService.Migrations;
using RentalCarService.Models;
using System.Collections.Generic;
using System.Linq;

namespace RentalCarService.Services
{
    public class UserService : IUserService
    {
        private readonly RentalCarsDBContext _dbContext;

        public UserService(RentalCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void InserNewUser(User User)
        {
            User.Nationality = FindIdCountryDB(User.Nationality.Id);
            User.CountryCNH = FindIdCountryDB(User.CountryCNH.Id);
            User.Address.Country = FindIdCountryDB(User.Address.Country.Id);

            _dbContext.Add(User);
            _dbContext.SaveChanges();
        }

        public List<User> ReadUsersFromDB()
        {
            var Users = _dbContext.Users.Include(N => N.Nationality).ToList();
            Users = _dbContext.Users.Include(CNH => CNH.CountryCNH).ToList();
            Users = _dbContext.Users.Include(A => A.Address.Country).ToList();

            return Users;
        }

        public void DeleteUser(int Id)
        {
            _dbContext.Remove(FindIdUserAddressDB(Id));
            _dbContext.SaveChanges();

            User toRemove = _dbContext.Users.Find(Id);
            _dbContext.Remove(toRemove);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User User)
        {
            User toUpdate = _dbContext.Users.Find(User.Id);
            toUpdate.Nationality = FindIdCountryDB(User.Nationality.Id);
            toUpdate.CountryCNH= FindIdCountryDB(User.CountryCNH.Id) ;
            toUpdate.Address = FindIdUserAddressDB(User.Id);
            toUpdate.Name= User.Name;
            toUpdate.Phone= User.Phone;
            toUpdate.IdentityDocument= User.IdentityDocument;
            toUpdate.Birthday= User.Birthday;
            toUpdate.Gender = User.Gender;
            toUpdate.CNH = User.CNH;
            toUpdate.DateCNH= User.DateCNH;
            toUpdate.Email= User.Email;
            toUpdate.Password= User.Password;
            _dbContext.SaveChanges();
        }

        private Countries FindIdCountryDB(int Id)
        {
            Countries Country = _dbContext.Countries.Find(Id);
            return Country;
        }
        
        private UserAddress FindIdUserAddressDB(int Id)
        {
            User User = _dbContext.Users.Include(A => A.Address).Where(I => I.Id == Id).FirstOrDefault();
            return User.Address;
        }
        
    }
}
