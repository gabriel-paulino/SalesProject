using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.ViewModels
{
    public class CreateAddressViewModel
    {
        [Required(ErrorMessage = "O campo 'Cep' é obrigatório")]
        [Display(Name = "Cep")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "O campo 'Tipo endereço' é obrigatório")]
        [Display(Name = "Tipo endereço")]
        public string Type { get; set; }
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

    }
}
