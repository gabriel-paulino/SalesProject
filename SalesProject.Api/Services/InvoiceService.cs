﻿using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Collections.Generic;

namespace SalesProject.Api.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public InvoiceService(
            IInvoiceRepository invoiceRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow)
        {
            _invoiceRepository = invoiceRepository;
            _orderRepository = orderRepository;
            _uow = uow;
        }

        public Invoice Get (Guid id) =>
            _invoiceRepository.Get(id);
        
        public Invoice CreateBasedInOrder(Order order)
        {
            var invoice = new Invoice(order);
            var invoiceLines = new List<InvoiceLine>();

            foreach (var line in order.OrderLines)
            {
                var taxLine = new TaxLine().GetDefaultTaxes(line.TotalPrice);
                var invoiceLine = new InvoiceLine(orderLine: line, taxLine: taxLine);

                invoiceLines.Add(invoiceLine);
            }

            foreach (var line in invoiceLines)
                invoice.AddInvoiceLine(line);

            if (!invoice.Valid)
                return invoice;

            order.Bill();

            _orderRepository.Update(order);
            _invoiceRepository.Create(invoice);

            _uow.Commit();

            return invoice;
        }
    }
}