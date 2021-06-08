using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "O campo 'Senha atual' é obrigatório")]
        [Display(Name = "Senha atual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "O campo 'Nova senha' é obrigatório")]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "O campo 'Confirme sua nova senha' é obrigatório")]
        [Display(Name = "Confirme sua nova senha")]
        public string ConfirmPassword { get; set; }
    }
}