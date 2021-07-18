using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Contact
{
    public class EditContactViewModel
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "WhatsApp")]
        public string WhatsApp { get; set; }

        [Required(ErrorMessage = "O campo 'Telefone' é obrigatório")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
    }
}