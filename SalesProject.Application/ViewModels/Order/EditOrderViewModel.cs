﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesProject.Application.ViewModels.Order
{
    public class EditOrderViewModel
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
        public List<EditOrderLine> OrderLines { get; set; }
    }
    public class EditOrderLine
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "O campo 'Quantidade' é obrigatório")]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

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