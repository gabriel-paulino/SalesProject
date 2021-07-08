using SalesProject.Api.ViewModels.Order;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesProject.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IUnitOfWork uow)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _uow = uow;
        }

        public Order Create(object createOrderViewModel, bool isCustomer, string username)
        {
            var model = (CreateOrderViewModel)createOrderViewModel;
            var orderWithGenericError = new Order();

            if (isCustomer)
            {
                var user = _userRepository.GetByUsername(username);

                if (user.CustomerId != Guid.Parse(model.CustomerId))
                {
                    var companyName = _customerRepository.Get(Guid.Parse(user.CustomerId.ToString())).CompanyName;

                    string messageError = $@"Ops. Não é possível criar esse pedido. O usuário '{username}' apenas pode criar pedidos para cliente '{companyName}'.";

                    orderWithGenericError.AddNotification(messageError);

                    return orderWithGenericError;
                }
            }

            var customer = _customerRepository.GetCustomerWithAdressesAndContacts(Guid.Parse(model.CustomerId));

            if (customer is null)
            {
                orderWithGenericError.AddNotification("Ops. Não foi possível criar o Pedido de Venda. Cliente não existe.");
                return orderWithGenericError;
            }

            var order =
                    new Order(
                        postingDate: DateTime.Parse(model.PostingDate).Date,
                        deliveryDate: DateTime.Parse(model.DeliveryDate).Date,
                        observation: model.Observation,
                        customer: customer
                        );

            if (!order.Valid)
                return order;

            var customerErrors = new List<string>();

            foreach (var line in model.OrderLines)
            {
                var product = _productRepository.Get(Guid.Parse(line.ProductId));

                if (product is null)
                {
                    order.AddNotification($"Ops. Produto com Id:'{line.ProductId}' não existe.");
                    return order;
                }

                if (product.CustomerId != customer.Id)
                {
                    string errorMessage = $"Ops. Não é possível criar esse pedido. " +
                        $"O produto '{product.Name}' não pertence ao cliente '{customer.CompanyName}'";

                    order.AddNotification(errorMessage);
                    return order;
                }

                var orderLine =
                    new OrderLine(
                                   quantity: line.Quantity,
                                   unitaryPrice: line.UnitaryPrice,
                                   additionalCosts: line.AdditionalCosts,
                                   product: product
                                   );

                if(!orderLine.Valid)
                {
                    order.AddNotification(orderLine.GetAllNotifications());
                    return order;
                }

                if (isCustomer)
                {
                    if (product.CombinedQuantity != orderLine.Quantity)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir quantidade igual à '{product.CombinedQuantity}'.");

                    if (product.CombinedPrice != orderLine.UnitaryPrice)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir preço unitário igual à '{product.CombinedPrice.ToString("C2")}'.");

                    if (product.AdditionalCosts != orderLine.AdditionalCosts)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir custos adicionais igual à '{product.AdditionalCosts.ToString("C2")}'.");
                }

                order.AddOrderLine(orderLine);
            }

            if (isCustomer && customerErrors.Any())
            {
                string errorMessage = string.Empty;

                foreach (var error in customerErrors)
                    errorMessage = $"{errorMessage} " +
                                   $"{error}";

                errorMessage = $"Ops... Algo deu errado: " +
                    $"{errorMessage} " +
                    $"Conforme definido no contrato.";

                order.AddNotification(errorMessage);
            }

            if (!order.Valid)
                return order;

            _orderRepository.Create(order);
            _uow.Commit();

            return order;
        }

        public Order Get(Guid id) =>
            _orderRepository.Get(id);

        public OrderFilter GetFilterToDashBoard(object orderFilterViewModel)
        {
            var model = (OrderFilterViewModel)orderFilterViewModel;

            return model.Status is not null
                ? new OrderFilter(
                    customerId: model.CustomerId,
                    status: (OrderStatus)model.Status,
                    startDate: model.StartDate,
                    endDate: model.EndDate
                    )
                : new OrderFilter(
                    customerId: model.CustomerId,
                    startDate: model.StartDate,
                    endDate: model.EndDate
                    );
        }

        public ICollection<Order> GetOrdersUsingFilter(OrderFilter filter) =>
            _orderRepository.GetOrdersUsingFilter(filter);
    }
}