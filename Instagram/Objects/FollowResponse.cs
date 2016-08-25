namespace Ninja.Instagram.API.Objects
{
    internal sealed class FollowResponse
    {
        public string status { get; set; }
        public Friendship_Status friendship_status { get; set; }
    }

    internal sealed class Friendship_Status
    {
        public bool incoming_request { get; set; }
        public bool followed_by { get; set; }
        public bool outgoing_request { get; set; }
        public bool following { get; set; }
        public bool blocking { get; set; }
        public bool is_private { get; set; }
    }

}
