using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IUserRepository : IDisposable
    {
        User Create(User user, string visiblePassword);
        User SignIn(User user, string visiblePassword);
        User ChangePassword(string username, string currentPassword, string newPassword);
        bool HasCustomerLink(Guid? customerId);
        bool HasAnotherUserSameUsernameOrEmail(User user);
    }
}