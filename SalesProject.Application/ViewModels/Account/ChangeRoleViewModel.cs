using System;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Account
{
    public class ChangeRoleViewModel
    {
        [Required(ErrorMessage = "O campo 'Função' é obrigatório")]
        [Display(Name = "Função")]
        [Range(1, 4)]
        public int Role { get; set; }
    }
}