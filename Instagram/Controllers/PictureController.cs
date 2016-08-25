using System.Net;
using Ninja.Instagram.API.Models;
using Ninja.Instagram.API.Extensions;

namespace Ninja.Instagram.API.Controllers
{
    public sealed class PictureController
    {
        #region Lookup
        public static Picture Lookup(string publicId)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://www.instagram.com/p/{publicId}/");
            request.SetHeaders("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");
            string source = request.GetSource();
            string id = source.Remove(0, source.IndexOf("\"id\": \"") + 7);
            id = id.Remove(id.IndexOf('"'));
            string caption = source.Remove(0, source.IndexOf("caption\": \"") + 11);
            caption = caption.Remove(caption.IndexOf("\", \"likes\""));
            return new Picture(id, publicId, caption);
        }
        #endregion

        #region Retrieve Pictures
        public static Picture[] GetPictures(string username)
        {
            return null;
        }

        public static Picture[] GetPictures(User user)
        {
            return null;
        }

        public static Picture[] GetPicturesByHashtag(string hashtag)
        {
            return null;
        }

        public static Picture[] GetPicturesByLocation(string location)
        {
            return null;
        }
        #endregion
    }
}