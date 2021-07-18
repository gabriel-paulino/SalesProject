using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo 'Usuário' é obrigatório")]
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
        [Display(Name = "Senha")]
        public string VisiblePassword { get; set; }
    }
}