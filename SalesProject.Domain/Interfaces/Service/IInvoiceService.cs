﻿using SalesProject.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IInvoiceService
    {
        Order GetOrderToCreateInvoice(Guid orderId);
        Invoice CreateBasedInOrder(Order order);
        void MarkAsIntegrated(Invoice invoice, string invoiceIdPlugNotas);
        Invoice Get(Guid id);
        Invoice GetByOrderId(Guid orderId);
        ICollection<Invoice> GetAllInvoicesAbleToSend();
        object GetInvoiceIdByOrderId(Guid orderId);
        object GetInvoiceIdOfPlugNotasByOrderId(Guid orderId);
    }
}