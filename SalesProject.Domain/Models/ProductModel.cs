using SalesProject.Domain.Entities;
using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Models
{
    public class ProductModel : Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NcmCode { get; set; }
        public decimal CombinedPrice { get; set; }
        public decimal AdditionalCosts { get; set; }
        public int CombinedQuantity { get; set; }
        public string Details { get; set; }
        public Guid? CustomerId { get; set; }

        public static implicit operator ProductModel(Product entity) =>
            new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                NcmCode = entity.NcmCode,
                CombinedPrice = entity.CombinedPrice,
                AdditionalCosts = entity.AdditionalCosts,
                CombinedQuantity = entity.CombinedQuantity,
                Details = entity.Details,
                CustomerId = entity.CustomerId
            };

        public static implicit operator Product(ProductModel model) =>
            new Product
            (
                name: model.Name,
                ncmCode: model.NcmCode,
                combinedPrice: model.CombinedPrice,
                additionalCosts: model.AdditionalCosts,
                combinedQuantity: model.CombinedQuantity,
                details: model.Details,
                customerId: model.CustomerId
            );
    }
}