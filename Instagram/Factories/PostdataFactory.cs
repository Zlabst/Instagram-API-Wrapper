using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ninja.Instagram.API.Factories
{
    internal sealed class PostdataFactory
    {
        public static string Sign(string postdata, string key)
        {
            return $"signed_body={Encode(postdata, key)}.{Uri.EscapeDataString(postdata)}&ig_sig_key_version=4";
        }

        private static string Encode(string postdata, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(postdata)).Aggregate(new StringBuilder(), (sb, b) => sb.Append(b.ToString("x2"))).ToString();
            }
        }
    }
}
