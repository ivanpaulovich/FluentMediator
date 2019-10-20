namespace WebApi.Controllers.Register
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public decimal Balance { get; set; }
    }
}