using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Infra.Context;
using System;
using System.Collections.Generic;
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

        public User GetByCustomerId(Guid customerId) =>
            _context.Users.FirstOrDefault(u => u.CustomerId == customerId);

        public IEnumerable<User> GetUsersByName(string name) =>
            _context.Users.Where(x => x.Name.Contains(name)).ToList();

        public object GetAll() =>
                (from user in _context.Users
                 select new
                 {
                     user.Id,
                     user.Username,
                     user.Name,
                     user.Email,
                     user.Role,
                     user.CustomerId
                 }).ToList();

        public User SignIn(User user, string visiblePassword)
        {
            var userDB = _context.Users
                    .FirstOrDefault(u => u.Username.Equals(user.Username));

            if (userDB is null)
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

        public void Update(User user) =>
            _context.Entry<User>(user).State = EntityState.Modified;

        public bool HasCustomerLink(Guid? customerId) =>
           _context.Users
            .Where(u => u.CustomerId == customerId)
            .FirstOrDefault() is not null;

        public bool HasAnotherUserWithSameUsername(string username) =>
            _context.Users
            .Where(u => u.Username == username)
            .Any();

        public bool HasAnotherUserWithSameEmail(string email) =>
            _context.Users
            .Where(u => u.Email == email)
            .Any();

        public void Dispose()
        {
            if (!_disposed)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}