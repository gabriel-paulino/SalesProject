using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Helper;
using SalesProject.ViewModels;
using System;

namespace SalesProject.Controllers
{
    public class AddressController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUnitOfWork _uow;

        public AddressController(
            ICustomerRepository customerRepository,
            IAddressRepository addressRepository,
            IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var customers = _customerRepository.GetAll();

            var model = new CreateAddressViewModel
            {
                ZipCode = string.Empty,
                Type = 0,
                TypeOptions = EnumeratorExtension.GetSelectListEnumAddressType(),
                Street = string.Empty,
                Number = 0,
                Neighborhood = string.Empty,
                City = string.Empty,
                CustomerId = string.Empty,
                CustomerOptions = new SelectList(customers, "Id", "CompanyName")
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var customers = _customerRepository.GetAll();
                model.TypeOptions = EnumeratorExtension.GetSelectListEnumAddressType();
                model.CustomerOptions = new SelectList(customers, "Id", "CompanyName");
                return View(model);
            }

            var address =
                    new Address(
                        zipCode: model.ZipCode,
                        type: (Domain.Enums.AddressType)model.Type,
                        street: model.Street,
                        neighborhood: model.Neighborhood,
                        number: model.Number,
                        city: model.City,
                        state: model.State,
                        customerId: Guid.Parse(model.CustomerId));

            if (address.Notifications.Count > 0)
            {
                var customers = _customerRepository.GetAll();
                model.TypeOptions = EnumeratorExtension.GetSelectListEnumAddressType();
                model.CustomerOptions = new SelectList(customers, "Id", "CompanyName");
                //Exibir erro
                return View(model);
            }

            _addressRepository.Create(address);
            _uow.Commit();
            return RedirectToAction("Index");
        }
    }
}