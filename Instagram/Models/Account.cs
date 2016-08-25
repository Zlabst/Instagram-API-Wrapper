using System;
using System.Net;

namespace Ninja.Instagram.API.Models
{
    public sealed class Account
    {
        public string Username { get; }
        public string Password { get; }
        public string UserAgent { get; }
        public string Key { get; }
        public string CsrfToken { get; internal set; }
        public string Uid { get; internal set; }
        public Guid Guid { get; }
        public CookieContainer Session { get; internal set; }
        public WebProxy Proxy { get; }

        public Account(string username, string password, string userAgent, string key, Guid guid, WebProxy proxy)
        {
            Username = username;
            Password = password;
            UserAgent = userAgent;
            Key = key;
            Guid = guid;
            Proxy = proxy;
        }
    }
}
