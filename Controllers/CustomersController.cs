using Microsoft.AspNetCore.Mvc;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Services;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersService _customersService;
        public CustomersController(ApplicationDbContext context)
        {
            _customersService = new CustomersService(context);
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = _customersService.GetById(id);

            if (customer == null)
            {
                return Content("User not found");
            }

            return View(customer);
        }

        public IActionResult New()
        {
            var membershipTypes = _customersService.GetMemberships();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customersService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerFormViewModel(customer)
            {
                MembershipTypes = _customersService.GetMemberships()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = _customersService.GetMemberships()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _customersService.Create(customer);

            }
            else
            {
                _customersService.Edit(customer);
            }

 
            return RedirectToAction("Index", "Customers");
        }
    }
}