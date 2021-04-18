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
        public string CombinedPrice { get; set; }

        [Display(Name = "Custos adicionais")]
        public string AdditionalCosts { get; set; }

        [Required(ErrorMessage = "O campo 'Quantidade combinada' é obrigatório")]
        [Display(Name = "Quantidade combinada")]
        public string CombinedQuantity { get; set; }

        [Display(Name = "Detalhes")]
        public string Details { get; set; }

        [Required(ErrorMessage = "O campo 'Cliente' é obrigatório")]
        [Display(Name = "Cliente")]
        public string CustomerId { get; set; }
    }
}