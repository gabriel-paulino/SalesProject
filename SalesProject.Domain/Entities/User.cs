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
            string name,
            string email,
            Guid? customerId)
        {
            this.Username = string.IsNullOrEmpty(username) ? email : username;
            this.Name = name;
            this.Email = email;
            this.Role = RoleType.Customer;
            this.CustomerId = customerId;

            DoValidations();
        }

        public User(
            string username,
            string name,
            string email,
            RoleType role)
        {
            this.Username = string.IsNullOrEmpty(username) ? email : username;
            this.Name = name;
            this.Email = email;
            this.Role = role;

            DoValidations();
        }

        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public RoleType Role { get; private set; }
        public Guid? CustomerId { get; private set; }

        public bool IsCustomer() => this.Role == RoleType.Customer;

        public void EncryptPassword(string passwordHash) =>
            this.PasswordHash = passwordHash;

        public void ChangeRole(RoleType role)
        {
            if (this.Role == RoleType.Customer)
            {
                AddNotification($"Ops. Não é possível atribur uma função de 'Funcionário' para um cliente.");
                return;
            }
            if (role == RoleType.Customer)
            {
                AddNotification($"Ops. Não é possível atribur a função de 'Cliente' para um funcionário.");
                return;
            }
                
            this.Role = role;
            DoValidations();
        }

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
            if (string.IsNullOrEmpty(Name))
                AddNotification("O preenchimento do campo 'Nome' é obrigatório.");
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
                Role != RoleType.Administrator)
                AddNotification("A 'Função' informada é inválida.");
        }
    }
}