using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapper.Models
{
    public class WebScarpDatabaseSettings: IWebScarpDatabaseSettings
    {
        public string WebScarpCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IWebScarpDatabaseSettings
    {
        string WebScarpCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
