﻿using SalesProject.Domain.Entities.Base;
using System;

namespace SalesProject.Domain.Entities
{
    public class OrderLine : BaseEntity
    {
        public OrderLine() { }

        public OrderLine(
            int quantity,
            decimal unitaryPrice,
            decimal additionalCosts,
            Product product)
        {
            this.Quantity = quantity;
            this.UnitaryPrice = unitaryPrice;
            this.AdditionalCosts = additionalCosts;
            this.Product = product;

            DoValidations();
            CalculateTotalLinePrice();
        }

        public Guid OrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitaryPrice { get; private set; }
        public decimal AdditionalCosts { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid? ProductId { get; private set; }
        public Product Product { get; private set; }

        public OrderLine Edit(
            int quantity,
            decimal unitaryPrice,
            decimal additionalCosts,
            Product product)
        {
            this.Quantity = quantity;
            this.UnitaryPrice = unitaryPrice;
            this.AdditionalCosts = additionalCosts;
            this.Product = product;

            DoValidations();
            CalculateTotalLinePrice();

            return this;
        }

        public override void DoValidations() =>
            ValidateNumericFields();
        
        private void ValidateNumericFields()
        {
            if (Quantity <= 0)
                AddNotification($"A Quantidade informada para '{Product.Name}' é inválida.");
            if (UnitaryPrice <= 0)
                AddNotification($"O Valor unitário informado para '{Product.Name}' é inválido.");
            if (AdditionalCosts < 0)
                AddNotification($"Os Custos adicionais informado para '{Product.Name}' é inválido.");
        }

        private void CalculateTotalLinePrice()
        {
            if (!Valid)
                return;

            TotalPrice = (decimal)Quantity * (UnitaryPrice + AdditionalCosts);
        }
    }
}