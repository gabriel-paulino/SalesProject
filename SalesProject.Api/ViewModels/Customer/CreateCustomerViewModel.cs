using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Customer
{
    public class CreateCustomerViewModel
    {
        [Required(ErrorMessage = "O campo 'Cnpj' é obrigatório")]
        [Display(Name = "Cnpj")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        [Display(Name = "Nome")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo 'Inscrição Estadual' é obrigatório")]
        [Display(Name = "Inscrição estadual")]
        public string StateRegistration { get; set; }

        [Display(Name = "Data de abertura")]
        public string Opening { get; set; }

        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Inscrição municipal")]
        public string MunicipalRegistration { get; set; }
    }
}