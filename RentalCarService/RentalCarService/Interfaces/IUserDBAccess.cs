﻿using RentalCarService.Models;

namespace RentalCarService.Interfaces
{
    public interface IUserDBAccess
    {
        public User GetUserById(int id);
        public User GetUserByIdThenInclude(int id);
    }
}
