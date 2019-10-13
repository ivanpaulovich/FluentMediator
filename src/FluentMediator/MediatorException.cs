using System;

namespace FluentMediator
{
    public class MediatorException : Exception
    {
        public MediatorException() { }
        public MediatorException(string message) : base(message) { }
        public MediatorException(string message, System.Exception inner) : base(message, inner) { }
    }
}