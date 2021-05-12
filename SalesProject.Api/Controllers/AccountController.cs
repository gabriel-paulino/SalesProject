using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Account;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly ITokenService _tokenService;

        public AccountController(
            IUserRepository userRepository,
            ITokenService tokenService,
            IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _uow = uow;
        }

        /// <summary>
        /// Logs in to the system and sends the jwt token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [AllowAnonymous]
        [Route("api/[controller]/login")]       
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

        /// <summary>
        /// Register an Account in the system and sends the jwt token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/register")]
        public ActionResult<dynamic> Register([FromBody] RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
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

            if (userTemp.IsCustomer() && _userRepository.HasCustomerLink(userTemp.CustomerId))
                return ValidationProblem(
                    detail: $"Ops. O Cliente com Id: '{userTemp.CustomerId}' já possuí um usuário no sistema");

            var user = _userRepository.Create(userTemp, model.Password);
            _uow.Commit();

            var token = _tokenService.GenerateToken(user);
            user.HidePasswordHash();

            return new
            {
                user = user,
                token = token
            };
        }

        /// <summary>
        /// Change the Password of the current user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/change-password")]        
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return ValidationProblem(detail: "Ops. As senhas não coincidem. Tente novamente.");

            var username = User.Identity.Name;
            var user = _userRepository.ChangePassword(username, model.CurrentPassword, model.NewPassword);

            if(!user.Valid)
                return ValidationProblem(detail: $"{user.GetNotification()}");

            _uow.Commit();

            user.HidePasswordHash();
            return Ok(user);
        }
    }
}