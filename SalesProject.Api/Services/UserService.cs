using SalesProject.Api.ViewModels.Account;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public User ChangePassword(string username, object changePasswordViewModel)
        {
            var model = (ChangePasswordViewModel)changePasswordViewModel;
            var user = _userRepository.ChangePassword(username, model.CurrentPassword, model.NewPassword);

            if (!user.Valid)
                return user;

            _uow.Commit();
            user.HidePasswordHash();

            return user;
        }

        public User ChangeRole(Guid id, RoleType newRole)
        {
            var user = _userRepository.Get(id);

            user.ChangeRole(newRole);

            if (!user.Valid)
                return user;

            _uow.Commit();
            user.HidePasswordHash();

            return user;
        }

        public bool Delete(Guid id)
        {
            var user = _userRepository.Get(id);

            if (user is null)
                return false;

            _userRepository.Delete(user);
            _uow.Commit();

            return true;
        }

        public User Edit(string username, object editUserViewModel)
        {
            var model = (EditUserViewModel)editUserViewModel;
            var user = _userRepository.GetByUsername(username);

            user.Edit(
                newName: model.NewName,
                newEmail: model.NewEmail);

            if (!user.Valid)
                return user;

            if (_userRepository.HasAnotherUserWithSameEmail(user.Email))
            {
                user.AddNotification($"Ops. Esse E-mail já está em uso.");
                return user;
            }

            _userRepository.Update(user);
            _uow.Commit();

            user.HidePasswordHash();
            return user;
        }

        public User Get(Guid id)
        {
            var user = _userRepository.Get(id);

            if (user is not null)
            {
                user.HidePasswordHash();
                return user;
            }
            return null;
        }

        public object GetAll() => _userRepository.GetAll();

        public User GetByCustomerId(Guid customerId)
        {
            var user = _userRepository.GetByCustomerId(customerId);

            if (user is not null)
            {
                user.HidePasswordHash();
                return user;
            }
            return null;
        }

        public IEnumerable<User> GetUsersByName(string name)
        {
            var users = _userRepository.GetUsersByName(name);

            if (users.Any())
            {
                foreach (var user in users)
                    user.HidePasswordHash();

                return users;
            }
            return null;
        }

        public User Login(object loginViewModel)
        {
            var model = (LoginViewModel)loginViewModel;
            var userTemp = new User(username: model.Username);

            var user = _userRepository.SignIn(userTemp, model.VisiblePassword);

            if (!user.Valid)
                return user;

            user.HidePasswordHash();
            return user;
        }

        public User Register(object registerViewModel)
        {
            var model = (RegisterViewModel)registerViewModel;

            var userTemp = (RoleType)model.Role == RoleType.Customer
                ? new User(
                    username: model.Username,
                    name: model.Name,
                    email: model.Email,
                    customerId: Guid.Parse(model.CustomerId))
                : new User(
                    username: model.Username,
                    name: model.Name,
                    email: model.Email,
                    role: (RoleType)model.Role)
                ;

            if (!userTemp.Valid)
                return userTemp;

            if (_userRepository.HasAnotherUserWithSameUsername(userTemp.Username))
            {
                userTemp.AddNotification($"Ops. Já existe um usuário com esse Username. Utilize um diferente.");
                return userTemp;
            }
            if (_userRepository.HasAnotherUserWithSameEmail(userTemp.Email))
            {
                userTemp.AddNotification($"Ops. Já existe um usuário com esse E-mail. Utilize um diferente.");
                return userTemp;
            }
            if (userTemp.IsCustomer() && _userRepository.HasCustomerLink(userTemp.CustomerId))
            {
                userTemp.AddNotification($"Ops. O Cliente com Id: '{userTemp.CustomerId}' já possuí um usuário no sistema.");
                return userTemp;
            }

            var user = _userRepository.Create(userTemp, model.Password);
            _uow.Commit();
            user.HidePasswordHash();

            return user;
        }
    }
}