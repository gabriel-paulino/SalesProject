using Microsoft.AspNetCore.Mvc;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Models;
using System.Diagnostics;

namespace SalesProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _uow;

        public CustomerController(ICustomerRepository customerRepository,
                                  IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
            _uow = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All()
        {
            var customers = _customerRepository.GetAll();
            return View(customers);
        }

        public ActionResult Create(Customer customer)
        {
            var error = customer.GetNotification();

            if (!string.IsNullOrEmpty(error))
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            try
            {
                _customerRepository.Create(customer);
                _uow.Commit();
            }
            catch
            {
                _uow.Rollback();
            }

            return View(customer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}