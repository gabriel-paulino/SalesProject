using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesProject.Api.ViewModels.Address
{
    public class CreateAddressViewModel
    {
        public CreateAddressViewModel()
        {
            this.TypeOptions = GetTypeOptions();
            //Todo: Descobrir um meio para preencher o CustomerOptions
        }

        [Required(ErrorMessage = "O campo 'Cep' é obrigatório")]
        [Display(Name = "Cep")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo endereço' é obrigatório")]
        [Display(Name = "Tipo endereço")]
        public int Type { get; set; }
        public SelectList TypeOptions { get; set; }

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

        [Required(ErrorMessage = "O campo 'Uf' é obrigatório")]
        [Display(Name = "Uf")]
        [StringLength(maximumLength: 2, MinimumLength = 2)]
        public string State { get; set; }

        [Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
        public SelectList CustomerOptions { get; set; }

        private SelectList GetTypeOptions() =>
                new SelectList(Enum.GetValues(typeof(AddressType)).
                                 Cast<AddressType>().
                                 Select(t => new SelectListItem
                                 {
                                     Text = t.ToString(),
                                     Value = ((int)t).ToString()
                                 }
                                 ).ToList(),
                                 "Value", "Text"
                                 );
    }

    public enum AddressType
    {
        Cobrança = 1,
        Entrega = 2,
        Outro = 3
    }
}