using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Customer
{
    public class EditCustomerViewModel
    {
        [Required(ErrorMessage = "O campo 'Inscrição Estadual' é obrigatório")]
        [Display(Name = "Inscrição estadual")]
        public string StateRegistration { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Inscrição municipal")]
        public string MunicipalRegistration { get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}