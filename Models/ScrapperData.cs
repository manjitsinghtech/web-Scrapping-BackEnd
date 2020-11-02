using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapper.Models
{
    public class ScrapperData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string siteName { get; set; }
        public string metaDescription { get; set; }
        public string metaKeywords { get; set; }
        public string imagePath { get; set; }
        public string title { get; set; }
        public List<string> hyperlinks { get; set; }
        
    }
}
