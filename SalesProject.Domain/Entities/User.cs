using SalesProject.Domain.Entities.Base;
using SalesProject.Domain.Enums;
using System;

namespace SalesProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string username) =>
            this.Username = username;

        public User(
            string username,
            string email,
            Guid? customerId)
        {
            this.Username = string.IsNullOrEmpty(username) ? email : username;
            this.Email = email;
            this.Role = RoleType.Customer;
            this.CustomerId = customerId;

            DoValidations();
        }

        public User(
            string username,
            string email,
            RoleType role)
        {
            this.Username = string.IsNullOrEmpty(username) ? email : username;
            this.Email = email;
            this.Role = role;

            DoValidations();
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public RoleType Role { get; private set; }
        public Guid? CustomerId { get; private set; }

        public bool IsCustomer() => this.Role == RoleType.Customer;

        public void EncryptPassword(string passwordHash) =>
            this.PasswordHash = passwordHash;

        public void HidePasswordHash() =>
            this.PasswordHash = string.Empty;

        public override void DoValidations()
        {
            ValidateFillingMandatoryFields();
            ValidateEmail();
            ValidateRole();
        }

        private void ValidateFillingMandatoryFields()
        {
            if (string.IsNullOrEmpty(Username))
                AddNotification("O preenchimento do campo 'Usuário' é obrigatório.");
            if (string.IsNullOrEmpty(Email))
                AddNotification("O preenchimento do campo 'E-mail' é obrigatório.");
        }

        private void ValidateEmail()
        {
            if (!Validation.Validation.IsValidEmail(Email))
                AddNotification("O 'E-mail' informado é inválido.");
        }

        private void ValidateRole()
        {
            if (Role != RoleType.Customer &&
                Role != RoleType.Seller &&
                Role != RoleType.It &&
                Role != RoleType.Administrator &&
                Role != RoleType.Other)
                AddNotification("A 'Função' informada é inválida.");
        }
    }
}