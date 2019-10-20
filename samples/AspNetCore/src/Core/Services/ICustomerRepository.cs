using Core.Wallet;

namespace Core.Services
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
    }
}