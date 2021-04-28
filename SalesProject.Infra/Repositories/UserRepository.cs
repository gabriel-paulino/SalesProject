using Microsoft.AspNetCore.Identity;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Linq;

namespace SalesProject.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private bool _disposed = false;

        public UserRepository(DataContext context) =>
            _context = context;

        ~UserRepository() =>
            Dispose();

        public User SignIn(User user, string visiblePassword)
        {
            var userDB = _context.Users
                    .FirstOrDefault(u => u.Username.Equals(user.Username));

            if (userDB == null)
            {
                user.AddNotification("Ops. Usuário não encontrado");
                return user;
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, userDB.PasswordHash, visiblePassword);

            if (result == PasswordVerificationResult.Failed)
                userDB.AddNotification("Ops. Falha ao realizar Login.");

            return userDB;
        }

        public User CreateUser(User user, string readablePassword)
        {
            var hasher = new PasswordHasher<User>();

            user.EncryptPassword(hasher.HashPassword(user, readablePassword));

            return _context.Users.Add(user).Entity;
        }

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
