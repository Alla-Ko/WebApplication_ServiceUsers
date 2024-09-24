using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication_Services.Models
{
    public class User
    {
        [BsonId]
        public string? Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;


    }
}
