using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace WebScrapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScrapperController : ControllerBase
    {
        [HttpGet]
        private async Task<List<dynamic>> GetPageData()
        {
            string url= "http://techbitsolution.com";
            List< dynamic > results=null;
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            // Debug
            //_logger.LogInformation(document.DocumentElement.OuterHtml);

            var advertrows = document.QuerySelectorAll("a");

            //foreach (var row in advertrows)
            //{
            //    // Create a container object
            //    CarAdvert advert = new CarAdvert();

            //    // Use regex to get all the numbers from this string
            //    MatchCollection regxMatches = Regex.Matches(row.QuerySelector(".price").TextContent, @"\d+\.*\d+");
            //    uint.TryParse(string.Join("", regxMatches), out uint price);
            //    advert.Price = price;

            //    regxMatches = Regex.Matches(row.QuerySelector(".year").TextContent, @"\d+");
            //    uint.TryParse(string.Join("", regxMatches), out uint year);
            //    advert.Year = year;

            //    // Get the fuel type from the ad
            //    advert.Fuel = row.QuerySelector(".fuel").TextContent[0];

            //    // Make and model
            //    advert.MakeAndModel = row.QuerySelector(".make_and_model > a").TextContent;

            //    // Link to the advert
            //    advert.AdvertUrl = websiteUrl + row.QuerySelector(".make_and_model > a").GetAttribute("Href");

            //    results.Add(advert);
            //}

            //// Check if a next page link is present
            //string nextPageUrl = "";
            //var nextPageLink = document.QuerySelector(".next-page > .item");
            //if (nextPageLink != null)
            //{
            //    nextPageUrl = websiteUrl + nextPageLink.GetAttribute("Href");
            //}

            //// If next page link is present recursively call the function again with the new url
            //if (!String.IsNullOrEmpty(nextPageUrl))
            //{
            //    return await GetPageData(nextPageUrl, results);
            //}

            return results;
        }
    }
}
