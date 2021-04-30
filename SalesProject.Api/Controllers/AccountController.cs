﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SalesProject.Api.Services;
using SalesProject.Api.ViewModels.Account;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly TokenService _tokenService;

        public AccountController(
            IConfiguration configuration,
            IUserRepository userRepository,
            IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _tokenService = new TokenService(configuration["JwtKey"]);
            _uow = uow;
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody] LoginViewModel model)
        {
            var userTemp = new User(username: model.Username);

            var user = _userRepository.SignIn(userTemp, model.VisiblePassword);

            if (!user.Valid)
                return ValidationProblem(detail: $"{user.GetNotification()}");

            var token = _tokenService.GenerateToken(user);
            user.HidePasswordHash();

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("api/[controller]/register")]
        public ActionResult<dynamic> Register([FromBody] RegisterViewModel model)
        {
            if (model.Password != model.PasswordAgain)
                return ValidationProblem(detail: "Ops. As senhas não coincidem. Tente novamente.");

            var userTemp = (RoleType)model.Role == RoleType.Customer
                ? new User(
                    username: model.Username,
                    email: model.Email,
                    customerId: Guid.Parse(model.CustomerId))
                : new User(
                    username: model.Username,
                    email: model.Email,
                    role: (RoleType)model.Role)
                ;

            if (!userTemp.Valid)
                return ValidationProblem(detail: $"{userTemp.GetNotification()}");

            if(userTemp.IsCustomer() && _userRepository.HasCustomerLink(userTemp.CustomerId))
                return ValidationProblem(
                    detail: $"Ops. O Cliente com Id: {userTemp.CustomerId} já possuí um usuário no sistema");

            var user = _userRepository.CreateUser(userTemp, model.Password);
            _uow.Commit();

            var token = _tokenService.GenerateToken(user);
            user.HidePasswordHash();

            return new
            {
                user = user,
                token = token
            };
        }
    }
}