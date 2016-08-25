namespace Ninja.Instagram.API.Models
{
    public sealed class User
    {
        public ulong ID { get; set; }
        public string Username { get; }

        public User(ulong id, string username)
        {
            ID = id;
            Username = username;
        }
    }
}
