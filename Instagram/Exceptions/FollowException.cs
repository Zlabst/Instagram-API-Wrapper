using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja.Instagram.API.Exceptions
{
    internal sealed class FollowException : Exception
    {
        public FollowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
