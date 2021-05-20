using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public User Get(Guid id) =>
            _context.Users.Find(id);

        public User GetByUsername(string username) =>
            _context.Users.FirstOrDefault(u => u.Username == username);

        public User SignIn(User user, string visiblePassword)
        {
            var userDB = _context.Users
                    .FirstOrDefault(u => u.Username.Equals(user.Username));

            if (userDB == null)
            {
                user.AddNotification($"Ops. Usuário '{user.Username}' não foi encontrado");
                return user;
            }

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, userDB.PasswordHash, visiblePassword);

            if (result == PasswordVerificationResult.Failed)
                userDB.AddNotification("Ops. Falha ao realizar Login. Senha incorreta.");

            return userDB;
        }

        public User Create(User user, string visiblePassword)
        {
            var hasher = new PasswordHasher<User>();

            user.EncryptPassword(hasher.HashPassword(user, visiblePassword));

            return _context.Users.Add(user).Entity;
        }

        public User ChangePassword(string username, string currentPassword, string newPassword)
        {
            var user = GetByUsername(username);

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);

            if (result == PasswordVerificationResult.Failed)
            {
                user.AddNotification("Ops. Falha ao trocar senha. Senha atual incorreta.");
                return user;
            }

            user.EncryptPassword(hasher.HashPassword(user, newPassword));

           _context.Entry<User>(user).State = EntityState.Modified;

            return user;
        }

        public void Delete(User user) =>
            _context.Users.Remove(user);

        public bool HasCustomerLink(Guid? customerId) =>
           _context.Users
            .Where(u => u.CustomerId == customerId)
            .FirstOrDefault() != null;

        public bool HasAnotherUserSameUsernameOrEmail(User user) =>
            _context.Users
            .Where(u => u.Username == user.Username || u.Email == user.Email)
            .Any();

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}