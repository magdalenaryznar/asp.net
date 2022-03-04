using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Services
{
    public class CustomersService
    {

        private ApplicationDbContext _context;
        public CustomersService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public Customer GetById(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);

            return customer;
        }

        public IEnumerable<MembershipType> GetMemberships()
        {
            return _context.MembershipTypes.ToList();
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Edit(Customer customer)
        {
            var customerInDb = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == customer.Id);

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.HasNewsletterSubscribed = customer.HasNewsletterSubscribed;

            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include(c => c.MembershipType);
        }

        public void Delete(int id)
        {
            var customerInDb = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
