using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WebScrapper.Models
{
    public class WebScrapService
    {
        private readonly IMongoCollection<ScrapperData> _data;
        public WebScrapService(IWebScarpDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _data = database.GetCollection<ScrapperData>(settings.WebScarpCollectionName);
        }
        public ScrapperData Create(ScrapperData data)
        {
            _data.InsertOne(data);
            return data;
        }
    }

}
