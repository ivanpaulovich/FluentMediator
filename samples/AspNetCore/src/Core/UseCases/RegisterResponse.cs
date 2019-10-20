using System;

namespace Core.UseCases
{
    public class RegisterResponse
    {
        public Guid UserId { get; private set; }

        public RegisterResponse(Guid userId)
        {
            UserId = userId;
        }
    }
}