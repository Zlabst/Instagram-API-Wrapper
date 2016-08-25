namespace Ninja.Instagram.API.Objects
{
    internal sealed class LoginResponse
    {
        public Button[] buttons { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string error_title { get; set; }
        public bool invalid_credentials { get; set; }
    }

    internal sealed class Button
    {
        public string action { get; set; }
        public string title { get; set; }
    }

    internal sealed class LoginFailedResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}