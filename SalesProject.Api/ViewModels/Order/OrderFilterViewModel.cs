using System;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Order
{
    public class OrderFilterViewModel
    {
        [Display(Name = "Cliente")]
        public Guid? CustomerId { get; set; }

        [Display(Name = "Status")]
        [Range(1, 4)]
        public int? Status { get; set; }

        [Display(Name = "Data inicial")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Data final")]
        public DateTime? EndDate { get; set; }
    }
}