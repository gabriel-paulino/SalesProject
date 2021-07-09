using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IUserService
    {
        User Get(Guid id);
        object GetAll();
        User GetByCustomerId(Guid customerId);
        ICollection<User> GetUsersByName(string name);
        User Register(object registerViewModel);
        bool Delete(Guid id);
        User Edit(string username, object editUserViewModel);
        User ChangePassword(string username, object changePasswordViewModel);
        User ChangeRole(Guid id, RoleType newRole);
        User Login(object loginViewModel);
    }
}