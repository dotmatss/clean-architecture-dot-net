using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.Entities;

namespace Infrastructure.Entities
{
    public class UserDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PasswordHash")] // map Mongo field PasswordHash to this property
        public string Password { get; set; }

        public User ToDomain()
        {
            return new User
            {
                Email = this.Email,
                PasswordHash = this.Password
            };
        }

        public static UserDocument FromDomain(User user)
        {
            return new UserDocument
            {
                Email = user.Email,
                Password = user.PasswordHash
            };
        }
    }
}
