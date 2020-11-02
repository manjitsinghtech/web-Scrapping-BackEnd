using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AngleSharp;
using System.Net;
using System.Drawing;
using System.IO;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using OpenQA.Selenium;
using Microsoft.AspNetCore.Hosting;
using WebScrapper.Models;
using Microsoft.AspNetCore.Cors;

namespace WebScrapper.Controllers
{
   
    [Route("api/[controller]"), EnableCors("AppPolicy")]
    [ApiController]
    public class WebScrapController : ControllerBase
    {
        private IHostingEnvironment _env;
        private readonly WebScrapService _webScrapService;

        public WebScrapController(IHostingEnvironment env, WebScrapService webScrapService)
        {
            _env = env;
            _webScrapService = webScrapService;
        }

        [HttpGet("[action]")]
        public async Task<ScrapperData>  scrapData([FromQuery] string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            ScrapperData scrapData = new ScrapperData();
            scrapData.hyperlinks= new List<string>();
            List<string> webSiteLinks = new List<string>();
            var advertrows = document.QuerySelectorAll("a");
            var title= document.QuerySelectorAll("title");
            var meta = document.QuerySelectorAll("meta");
            scrapData.siteName = url;
            foreach (var row in meta)
            {
                if (row.GetAttribute("Name")=="description")
                {
                    scrapData.metaDescription = row.GetAttribute("Content");
                }
                if (row.GetAttribute("Name") == "keywords")
                {
                   scrapData.metaKeywords = row.GetAttribute("Content");
                }
            }


            foreach (var row in advertrows)
            {

                var link = row.GetAttribute("Href");
                if (scrapData.hyperlinks != null)
                {
                    if (!scrapData.hyperlinks.Contains(link))
                    { scrapData.hyperlinks.Add(link); }
                }
                else
                {
                    scrapData.hyperlinks.Add(link);
                }
               
               
            }
         
            scrapData.title = title[0].InnerHtml;
            var webRoot = _env.WebRootPath;
            
            var PathWithFolderName = System.IO.Path.Combine("assets");
            if (!Directory.Exists(PathWithFolderName))
            { 
                DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
            }
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            driver.Navigate().GoToUrl(url);
            var screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(PathWithFolderName+"/screenshot" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png");
            //scrapData.imagePath = PathWithFolderName + "/screenshot" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png";
            scrapData.imagePath = PathWithFolderName+"/screenshot" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_tt)") + ".png";
            driver.Close();
            driver.Quit();
            _webScrapService.Create(scrapData);
            return scrapData;
        }
       
    }
}
