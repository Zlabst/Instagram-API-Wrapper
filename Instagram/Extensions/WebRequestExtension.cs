using System.IO;
using System.Net;

namespace Ninja.Instagram.API.Extensions
{
    internal static class WebRequestExtension
    {
        public static void SetHeaders(this HttpWebRequest request, string userAgent, CookieContainer session = null)
        {
            request.Headers.Add("X-IG-Connection-Type: WIFI");
            request.Headers.Add("X-IG-Capabilities: 3Q==");
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
            request.UserAgent = userAgent;

            if (session == null)
                request.CookieContainer = new CookieContainer();
            else
                request.CookieContainer = session;
        }

        public static void SetPost(this HttpWebRequest request, byte[] postdata)
        {
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.ContentLength = postdata.Length;

            using (Stream requestStream = request.GetRequestStream())
                requestStream.Write(postdata, 0, postdata.Length);
        }

        public static HttpWebResponse GetHttpResponse(this HttpWebRequest request)
        {
            return request.GetResponse() as HttpWebResponse;
        }

        public static string ReadResponse(this HttpWebResponse response)
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                return reader.ReadToEnd();
        }

        public static string GetSource(this HttpWebRequest request)
        {
            using (HttpWebResponse response = request.GetHttpResponse())
                return response.ReadResponse();
        }
    }
}
