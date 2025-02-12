using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APInet.Models
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string Brand { get; set; }
        public string Item { get; set; }
    }
}
