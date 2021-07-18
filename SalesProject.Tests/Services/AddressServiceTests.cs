using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SalesProject.Application.Services;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace SalesProject.Tests.Services
{
    [TestClass]
    public class AddressServiceTests
    {
        private readonly IAddressRepository _addressRepository;
        private readonly AddressService _addressService;
        private readonly IUnitOfWork _uow;
        private readonly Address _address;
        private readonly Guid _customerId;

        public AddressServiceTests()
        {
            _customerId = Guid.NewGuid();
            _addressRepository = Substitute.For<IAddressRepository>();
            _uow = Substitute.For<IUnitOfWork>();

            _addressService = new AddressService(_addressRepository, _uow);

            _address = new Address(
                description: "Balcão 1",
                zipCode: "02248-050",
                type: AddressType.Other,
                street: "Rua Francisca Maria de Souza",
                neighborhood: "Parada Inglesa",
                number: 131,
                city: "São Paulo",
                state: "SP",
                customerId: _customerId);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenGetAddresesByCustomerId()
        {
            var addreses = new List<Address> { _address };

            _addressRepository.GetByCustomerId(_customerId)
                .Returns(addreses);

            _addressService.GetByCustomerId(_customerId);

            _addressRepository
                .Received(requiredNumberOfCalls: 1)
                .GetByCustomerId(_customerId);
        }
    }
}