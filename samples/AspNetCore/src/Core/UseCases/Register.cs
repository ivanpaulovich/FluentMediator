using Core.Services;
using Core.Wallet;

namespace Core.UseCases
{
    public class Register : IRegisterUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public Register(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public RegisterResponse Execute(RegisterCommand registerCommand)
        {
            var customer = new Customer(
                registerCommand.Name,
                registerCommand.SSN,
                registerCommand.Balance
            );

            _customerRepository.Add(customer);

            return new RegisterResponse(customer.Id);
        }
    }
}