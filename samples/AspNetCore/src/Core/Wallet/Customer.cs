using System;

namespace Core.Wallet
{
    public class Customer
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string SSN { get; protected set; }
        public decimal Balance { get; protected set; }
        public DateTime Timestamp { get; protected set; }

        public Customer(string name, string ssn, decimal balance)
        {
            Id = Guid.NewGuid();
            Name = name;
            SSN = ssn;
            Balance = balance;
            Timestamp = DateTime.Now;
        }
    }
}