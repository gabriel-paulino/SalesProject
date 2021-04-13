using System.ComponentModel.DataAnnotations;

namespace SalesProject.Api.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Código ncm")]
        public string NcmCode { get; set; }

        [Required(ErrorMessage = "O campo 'Preço combinado' é obrigatório")]
        [Display(Name = "Preço combinado")]
        public decimal CombinedPrice { get; set; }

        [Display(Name = "Custos adicionais")]
        public decimal AdditionalCosts { get; set; }

        [Required(ErrorMessage = "O campo 'Quantidade combinada' é obrigatório")]
        [Display(Name = "Quantidade combinada")]
        public double CombinedQuantity { get; set; }

        [Display(Name = "Detalhes")]
        public string Details { get; set; }

        [Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
    }
}