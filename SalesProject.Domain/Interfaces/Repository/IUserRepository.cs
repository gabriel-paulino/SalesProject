using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IUserRepository : IDisposable
    {
        User Get(Guid id);
        User GetByUsername(string username);
        User GetByCustomerId(Guid customerId);
        ICollection<User> GetUsersByName(string name);
        object GetAll();
        User Create(User user, string visiblePassword);
        User SignIn(User user, string visiblePassword);
        User ChangePassword(string username, string currentPassword, string newPassword);
        void Delete(User user);
        void Update(User user);
        bool HasCustomerLink(Guid? customerId);
        bool HasAnotherUserWithSameUsername(string username);
        bool HasAnotherUserWithSameEmail(string email);
    }
}