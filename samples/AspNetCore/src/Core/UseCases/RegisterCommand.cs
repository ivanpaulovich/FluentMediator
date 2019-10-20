namespace Core.UseCases
{
    public class RegisterCommand
    {
        public string Name { get; private set; }
        public string SSN { get; private set; }
        public decimal Balance { get; private set; }

        public RegisterCommand(string name, string ssn, decimal balance)
        {
            Name = name;
            SSN = ssn;
            Balance = balance;
        }
    }
}