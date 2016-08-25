namespace Ninja.Instagram.API.Objects
{
    internal sealed class LikeResponse
    {
        public string status { get; set; }
    }

    internal sealed class LikeFailedResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
