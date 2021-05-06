using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IUserRepository : IDisposable
    {
        User CreateUser(User user, string visiblePassword);
        User SignIn(User user, string visiblePassword);
        User ChangePassword(string username, string currentPassword, string newPassword);
        bool HasCustomerLink(Guid? customerId);
    }
}