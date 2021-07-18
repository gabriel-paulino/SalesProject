using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Customer
{
    public class EditCompleteCustomerViewModel
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

        [Required]
        public List<EditCustomerAddress> Adresses { get; set; }

        [Required]
        public List<EditCustomerContact> Contacts { get; set; }

    }

    public class EditCustomerAddress
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo 'Descrição' é obrigatório")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo 'Cep' é obrigatório")]
        [Display(Name = "Cep")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo endereço' é obrigatório")]
        [Display(Name = "Tipo endereço")]
        [Range(1, 3)]
        public int Type { get; set; }

        [Required(ErrorMessage = "O campo 'Logradouro' é obrigatório")]
        [Display(Name = "Logradouro")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo 'Número' é obrigatório")]
        [Display(Name = "Número")]
        public int Number { get; set; }

        [Required(ErrorMessage = "O campo 'Bairro' é obrigatório")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }

        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo 'UF' é obrigatório")]
        [Display(Name = "UF")]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo 'Código Ibge cidade' é obrigatório")]
        [Display(Name = "Código Ibge cidade")]
        public string CodeCity { get; set; }
    }

    public class EditCustomerContact
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

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