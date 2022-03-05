using Microsoft.AspNetCore.Mvc;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Services;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace LibApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersService _customersService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _customersService = new CustomersService(context);
            _httpContextAccessor = httpContextAccessor;
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = GetCustomerDetailsFromApi(id).Result;

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

        private async Task<Customer> GetCustomerDetailsFromApi(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                var result = await httpClient.GetAsync($"{apiUrl}/api/customers/{id}");

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var content = await result.Content.ReadAsStringAsync();

                var customer = JsonConvert.DeserializeObject<Customer>(content);

                return customer;
            }
        }
    }
}