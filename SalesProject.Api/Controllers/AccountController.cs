using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Account;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(
            IUserService userService,
            ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Logs in to the system and sends the jwt token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/login")]
        public ActionResult<dynamic> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userService.Login(model);

            if (!user.Valid)
                return ValidationProblem(user.GetAllNotifications());

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                user = user,
                token = token
            });
        }

        /// <summary>
        /// Register an Account in the system and sends the jwt token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "It,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/register")]
        public ActionResult<dynamic> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Password != model.ConfirmPassword)
                return ValidationProblem("Ops. As senhas não coincidem. Tente novamente.");

            var user = _userService.Register(model);

            if (!user.Valid)
                return ValidationProblem(user.GetAllNotifications());

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                user = user,
                token = token
            });
        }

        /// <summary>
        /// Change the Password of the current logged User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/change-password")]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.NewPassword != model.ConfirmPassword)
                return ValidationProblem("Ops. As senhas não coincidem. Tente novamente.");

            string username = User.Identity.Name;
            var user = _userService.ChangePassword(username, model);

            if (!user.Valid)
                return ValidationProblem(user.GetAllNotifications());

            return Ok(user);
        }

        /// <summary>
        /// Change Name and E-mail of the current logged User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]")]
        public IActionResult Edit([FromBody] EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string username = User.Identity.Name;

            var user = _userService.Edit(username, model);

            if (!user.Valid)
                return ValidationProblem(user.GetAllNotifications());

            return Ok(user);
        }


        /// <summary>
        /// Delete an User by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "It,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            if (_userService.Delete(id))
                return Ok();

            return NotFound($"Ops. Usuário com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Change the Role of a specific User.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "It,Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("api/[controller]/change-role")]
        public IActionResult ChangeRole(Guid id, ChangeRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userService.ChangeRole(id, (RoleType)model.Role);

            if (!user.Valid)
                return ValidationProblem(user.GetAllNotifications());

            return Ok(user);
        }

        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "It,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();

            if (users is not null)
                return Ok(users);

            return NotFound($"Ops. Nenhum usuário foi cadastrado.");
        }

        /// <summary>
        /// Get an User by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "It,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _userService.Get(id);

            if (user is not null)
                return Ok(user);

            return NotFound($"Ops. Nenhum usuário com Id: '{id}' foi encontrado.");
        }

        /// <summary>
        /// Get an User by CustomerId.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "It,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{customerId:guid}")]
        public IActionResult GetUserByCustomerId(Guid customerId)
        {
            var user = _userService.GetByCustomerId(customerId);

            if (user is not null)
                return Ok(user);

            return NotFound($"Ops. Nenhum usuário com CustomerId: '{customerId}' foi encontrado.");
        }

        /// <summary>
        /// Get all users with Name contains this param.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "It,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/name/{name}")]
        public IActionResult GetUsersByName(string name)
        {
            var users = _userService.GetUsersByName(name);

            if (users is not null)
                return Ok(users);

            return NotFound($"Ops. Nenhum usuário com Nome: '{name}' foi encontrado.");
        }
    }
}