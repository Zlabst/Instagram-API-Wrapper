namespace Ninja.Instagram.API.Models
{
    public sealed class Picture
    {
        public string ID { get; }
        public string PublicID { get; }
        public string Description { get; }

        public Picture(string id, string publicId, string description)
        {
            ID = id;
            PublicID = publicId;
            Description = description;
        }
    }
}
