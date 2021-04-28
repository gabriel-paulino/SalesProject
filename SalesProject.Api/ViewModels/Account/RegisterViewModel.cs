using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O campo 'Usuário' é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo 'Repita a senha' é obrigatório")]
        [Display(Name = "Repita a senha")]
        public string PasswordAgain { get; set; }

        [Required(ErrorMessage = "O campo 'Função' é obrigatório")]
        [Display(Name = "Função")]
        public int Role { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
    }
}