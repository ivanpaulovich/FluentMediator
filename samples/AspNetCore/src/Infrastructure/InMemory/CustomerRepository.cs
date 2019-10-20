using System.Collections.Generic;
using Core.Services;
using Core.Wallet;

namespace Infrastructure.InMemory
{
    public class CustomerRepository : ICustomerRepository
    {
        public IList<Customer> Customers { get; }

        public CustomerRepository()
        {
            Customers = new List<Customer>();
        }

        public void Add(Customer customer)
        {
            Customers.Add(customer);
        }
    }
}