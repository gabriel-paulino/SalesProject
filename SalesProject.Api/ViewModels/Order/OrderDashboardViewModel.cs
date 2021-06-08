using System;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Order
{
    public class OrderDashboardViewModel
    {
        [Display(Name = "Data inicial")]
        [Required(ErrorMessage = "O campo 'Data inicial' é obrigatório")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Data final")]
        [Required(ErrorMessage = "O campo 'Data final' é obrigatório")]
        public DateTime EndDate { get; set; }
    }
}