using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using Ninja.Instagram.API.Factories;
using Ninja.Instagram.API.Models;
using Ninja.Instagram.API.Exceptions;
using Ninja.Instagram.API.Extensions;
using Ninja.Instagram.API.Objects;

namespace Ninja.Instagram.API.Controllers
{
    public sealed class AccountController
    {
        private static JavaScriptSerializer _deserializer;

        static AccountController()
        {
            if (_deserializer == null)
                _deserializer = new JavaScriptSerializer();
        }

        #region API
        #region Create
        public static Account Create(string username, string password, string email, string name, WebProxy proxy = null)
        {
            Account account = GenerateAccount(username, password, proxy);

            SendRegistrationRequest(account, email, name);

            return account;
        }

        private static void SendRegistrationRequest(Account account, string email, string name)
        {
            try
            {
                string postdata = PostdataFactory.Sign($"{{\"phone_id\":\"{Guid.NewGuid().ToString("D")}\",\"_csrftoken\":\"{account.CsrfToken}\",\"username\":\"{account.Username}\",\"first_name\":\"{name}\",\"guid\":\"{account.Guid.ToString("D")}\",\"device_id\":\"android-{Guid.NewGuid().ToString("N").Substring(0, 16)}\",\"email\":\"{email}\",\"force_sign_up_code\":\"\",\"waterfall_id\":\"{Guid.NewGuid().ToString("D")}\",\"qs_stamp\":\"\",\"password\":\"{account.Password}\"}}", account.Key);
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                HttpWebRequest request = WebRequest.CreateHttp("https://i.instagram.com/api/v1/accounts/create/");
                request.SetHeaders(account.UserAgent, account.Session);
                request.Proxy = account.Proxy;
                request.SetPost(bytes);

                account.Session = request.CookieContainer;
                string source = request.GetSource();
                dynamic json = _deserializer.Deserialize<dynamic>(source);
                if (!json["account_created"])
                    throw new CreateException("Failed to create account. Could not create account.", null);
            }
            catch (Exception ex)
            {
                throw new CreateException("Failed to create account. Could not create account.", ex);
            }
        }
        #endregion

        #region Login
        public static Account Login(string username, string password, WebProxy proxy = null)
        {
            Account account = GenerateAccount(username, password, proxy);

            SendLoginRequest(account);

            return account;
        }

        private static void SendLoginRequest(Account account)
        {
            try
            {
                string postdata = PostdataFactory.Sign($"{{\"phone_id\":\"{Guid.NewGuid().ToString("D")}\",\"_csrftoken\":\"{account.CsrfToken}\",\"username\":\"{account.Username}\",\"guid\":\"{account.Guid.ToString("D")}\",\"device_id\":\"android-{Guid.NewGuid().ToString("N").Substring(0, 16)}\",\"password\":\"{account.Password}\",\"login_attempt_count\":\"0\"}}", account.Key);
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                HttpWebRequest request = WebRequest.CreateHttp("https://i.instagram.com/api/v1/accounts/login/");
                request.SetHeaders(account.UserAgent, account.Session);
                request.Proxy = account.Proxy;
                request.SetPost(bytes);

                string source = request.GetSource();
                LoginResponse json = _deserializer.Deserialize<LoginResponse>(source);
                if (json.status != "ok")
                    throw new LoginException("Failed to login. Could not login.", null);

                account.Session = request.CookieContainer;
            }
            catch (WebException wex)
            {
                CheckWebException<LoginException>(wex, "Failed to login. Could not login.");
            }
            catch (Exception ex)
            {
                throw new LoginException("Failed to login. Could not login.", ex);
            }
        }
        #endregion

        #region (Un)Follow
        public static void Follow(Account account, User user)
        {
            ChangeFollow(account, user);
        }

        public static void UnFollow(Account account, User user)
        {
            ChangeFollow(account, user, true);
        }

        private static void ChangeFollow(Account account, User user, bool unfollow = false)
        {
            try
            {
                string postdata = PostdataFactory.Sign($"{{\"_csrftoken\":\"{account.CsrfToken}\",\"user_id\":\"{user.ID}\",\"_uid\":\"{account.Uid}\",\"_uuid\":\"{account.Guid.ToString("D")}\"}}", account.Key);
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                HttpWebRequest request = WebRequest.CreateHttp($"https://i.instagram.com/api/v1/friendships/{(unfollow ? "destroy" : "create")}/{user.ID}/");
                request.SetHeaders(account.UserAgent, account.Session);
                request.Proxy = account.Proxy;
                request.SetPost(bytes);

                string source = request.GetSource();
                FollowResponse json = _deserializer.Deserialize<FollowResponse>(source);
                if (json.status != "ok" || json.friendship_status.following == unfollow)
                    throw new FollowException("Failed to follow. Could not follow.", null);
            }
            catch (WebException wex)
            {
                using (HttpWebResponse response = wex.Response as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        throw new FollowException("Failed to follow. User does not exist.", wex);
                }
            }
            catch (Exception ex)
            {
                throw new FollowException("Failed to follow. Could not follow.", ex);
            }
        }
        #endregion

        #region (Un)Like
        public static void Like(Account account, User user, Picture picture)
        {
            ChangeLike(account, user, picture);
        }

        public static void UnLike(Account account, User user, Picture picture)
        {
            ChangeLike(account, user, picture, true);
        }

        public static void ChangeLike(Account account, User user, Picture picture, bool unlike = false)
        {
            string media_id = $"{picture.ID}_{user.ID}";

            try
            {
                string postdata = PostdataFactory.Sign($"{{\"module_name\":\"feed_contextual_post\",\"media_id\":\"{media_id}\", \"_csrftoken\":\"{account.CsrfToken}\",\"_uid\":\"{account.Uid}\",\"_uuid\":\"{account.Guid.ToString("D")}\"}}", account.Key) + "&d=" + (unlike ? 1 : 0);
                byte[] bytes = Encoding.UTF8.GetBytes(postdata);
                HttpWebRequest request = WebRequest.CreateHttp($"https://i.instagram.com/api/v1/media/{media_id}/{(unlike ? "unlike" : "like")}/");
                request.SetHeaders(account.UserAgent, account.Session);
                request.Proxy = account.Proxy;
                request.SetPost(bytes);

                string source = request.GetSource();
                LikeResponse json = _deserializer.Deserialize<LikeResponse>(source);
                if (json.status != "ok")
                    throw new LikeException("Failed to (un)like. Could not (un)like.", null);
            }
            catch (WebException wex)
            {
                CheckWebException<LikeException>(wex, "Failed to (un)like. Could not (un)like.");
            }
            catch (Exception ex)
            {
                throw new LikeException("Failed to (un)like. Could not (un)like.", ex);
            }
        }
        #endregion

        #region (Un)Comment

        #endregion

        #region (Un)Post

        #endregion

        #region Report

        #endregion

        #region Direct Message

        #endregion
        #endregion

        #region Prepare Account
        private static Account GenerateAccount(string username, string password, WebProxy proxy = null)
        {
            Guid guid = Guid.NewGuid();
            KeyValuePair<string, string> version = VersionFactory.GetRandom();
            string csrfToken = string.Empty;
            string userAgent = UserAgentFactory.GetRandom(version.Value);
            Account account = new Account(username, password, userAgent, version.Key, guid, proxy);

            PrepareSession(account);

            return account;
        }

        private static void PrepareSession(Account account)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp($"https://i.instagram.com/api/v1/si/fetch_headers/?guid={account.Guid.ToString("N")}&challenge_type=signup");
                request.SetHeaders(account.UserAgent);
                request.Proxy = account.Proxy;

                using (HttpWebResponse response = request.GetHttpResponse())
                {
                    account.CsrfToken = response.Cookies[0].Value;
                    account.Session = request.CookieContainer;
                }
            }
            catch (Exception ex)
            {
                throw new LoginException("Failed to create session. Could not login.", ex);
            }
        }
        #endregion

        #region Miscellaneous
        private static void CheckWebException<Tex>(WebException wex, string exceptionMessage) where Tex : Exception
        {
            using (HttpWebResponse response = wex.Response as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string source = (wex.Response as HttpWebResponse).ReadResponse();
                    throw Activator.CreateInstance(typeof(Tex), wex.Message, wex) as Tex;
                }
                else
                    throw Activator.CreateInstance(typeof(Tex), exceptionMessage, wex) as Tex;
            }
        }
        #endregion
    }
}