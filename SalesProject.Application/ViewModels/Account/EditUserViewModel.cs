using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Account
{
    public class EditUserViewModel
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        [Display(Name = "Nome")]
        public string NewName{ get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [Display(Name = "E-mail")]
        public string NewEmail { get; set; }
    }
}