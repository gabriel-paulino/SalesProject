﻿using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Customer
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
    }
}