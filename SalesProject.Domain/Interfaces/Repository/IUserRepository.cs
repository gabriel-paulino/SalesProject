using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IUserRepository : IDisposable
    {
        User Get(Guid id);
        User GetByUsername(string username);
        User Create(User user, string visiblePassword);
        User SignIn(User user, string visiblePassword);
        User ChangePassword(string username, string currentPassword, string newPassword);
        void Delete(User user);
        bool HasCustomerLink(Guid? customerId);
        bool HasAnotherUserSameUsernameOrEmail(User user);
    }
}