using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "O campo 'Data lançamento' é obrigatório")]
        [Display(Name = "Data lançamento")]
        public string PostingDate { get; set; }

        [Required(ErrorMessage = "O campo 'Data de entrega' é obrigatório")]
        [Display(Name = "Data de entrega")]
        public string DeliveryDate { get; set; }

        [Display(Name = "Observação")]
        public string Observation { get; set; }

        [Required]
        public List<Lines> OrderLines { get; set; }

        [Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
    }

    public class Lines
    {
        [Required(ErrorMessage = "O campo 'Quantidade' é obrigatório")]
        [Display(Name = "Quantidade")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "O campo 'Preço unitário' é obrigatório")]
        [Display(Name = "Preço unitário")]
        public decimal UnitaryPrice { get; set; }

        [Display(Name = "Custos adicionais")]
        public decimal AdditionalCosts { get; set; }

        [Required(ErrorMessage = "O campo 'Produdo' é obrigatório")]
        [Display(Name = "Produto")]
        public string ProductId { get; set; }
    }
}