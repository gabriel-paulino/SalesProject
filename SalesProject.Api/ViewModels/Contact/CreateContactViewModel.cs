using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Contact
{
    public class CreateContactViewModel
    {
        public CreateContactViewModel() { }
        //Todo: Descobrir um meio para preencher o CustomerOptions

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

        [Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
        public SelectList CustomerOptions { get; set; }
    }
}