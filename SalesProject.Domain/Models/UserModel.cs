using SalesProject.Domain.Entities;
using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Models
{
    public class UserModel : Model
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RoleType Role { get; set; }
        public Guid? CustomerId { get; set; }

        public static implicit operator UserModel(User entity) =>
            entity is null ? default :
            new UserModel()
            {
                Id = entity.Id,
                Username = entity.Username,
                Name = entity.Name,
                Email = entity.Email,
                PasswordHash = entity.PasswordHash,
                Role = entity.Role,
                CustomerId = entity.CustomerId
            };

        public static implicit operator User(UserModel model) =>
            new User
            (
                username: model.Username,
                name: model.Name,
                email: model.Email,
                role: model.Role,
                customerId: model.CustomerId,
                id: model.Id
            );
    }
}
