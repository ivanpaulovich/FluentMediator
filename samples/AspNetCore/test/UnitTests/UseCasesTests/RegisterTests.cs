using Core.UseCases;
using Infrastructure.InMemory;
using Xunit;

namespace UnitTests.UseCasesTests
{
    public class RegisterTests
    {
        [Fact]
        public void Register_PublishesEvent()
        {
            // Arrange
            var customerRepository = new CustomerRepository();
            var sut = new Register(customerRepository);
            var registerCommand = new RegisterCommand(
                "Ivan Paulovich",
                "198608175555",
                460
            );
            
            // Act
            var response = sut.Execute(registerCommand);
            
            // Assert
            Assert.NotEmpty(customerRepository.Customers);
        }
    }
}