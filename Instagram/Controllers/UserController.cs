using System;
using System.Net;
using Ninja.Instagram.API.Models;
using Ninja.Instagram.API.Extensions;

namespace Ninja.Instagram.API.Controllers
{
    public sealed class UserController
    {
        #region Lookup
        public static User Lookup(string username)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://www.instagram.com/{username}/");
            request.SetHeaders("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            string source = request.GetSource();
            source = source.Remove(0, source.IndexOf("\"id\": \"") + 7);
            ulong id = Convert.ToUInt64(source.Remove(source.IndexOf('"')));
            return new User(id, username);
        }
        #endregion

        #region Retrieve Followers
        public static string[] GetFollowers(string username)
        {
            return null;
        }

        public static string[] GetFollowers(User user)
        {
            return null;
        }
        #endregion

        #region Retrieve Following
        public static string[] GetFollowing(string username)
        {
            return null;
        }

        public static string[] GetFollowing(User user)
        {
            return null;
        }
        #endregion
    }
}
