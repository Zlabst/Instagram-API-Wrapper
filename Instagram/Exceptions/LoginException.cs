using System;

namespace Ninja.Instagram.API.Exceptions
{
    internal sealed class LoginException : Exception
    {
        public LoginException(string message, Exception innerException) : base(message, innerException) { }
    }
}
