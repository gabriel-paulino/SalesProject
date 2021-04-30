using SalesProject.Domain.Entities;
using System;

namespace SalesProject.Domain.Interfaces.Repository
{
    public interface IUserRepository : IDisposable
    {
        User CreateUser(User user, string visiblePassword);
        User SignIn(User user, string visiblePassword);
        bool HasCustomerLink(Guid? customerId);
    }
}