using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AbeBooksController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.abebooks.com/servlet/SearchResults?kn=" + isbn +
                                          "&sts=t&cm_sp=SearchF-_-topnav-_-Results");
                var cover = driver.FindElement(By.XPath("//*[@id='listing_1']/div/img")).GetAttribute("src");
                var ISBN = driver.FindElement(By.XPath(
                    "/html/body/div[2]/main/div[6]/div[1]/div[2]/ul/li[1]/div[2]/div[1]/div[1]/p[3]/a/span[2]")).Text;
                ISBN = ISBN.Substring(9, ISBN.Length - 10);
                var editorString = driver.FindElement(By.XPath(
                    "/html/body/div[2]/main/div[6]/div[1]/div[2]/ul/li[1]/div[2]/div[1]/div[1]/p[1]")).Text;
                var authors = editorString.Split(new[] {";", "-", "/"}, StringSplitOptions.RemoveEmptyEntries);
                var title = driver
                    .FindElement(By.XPath(
                        "/html/body/div[2]/main/div[6]/div[1]/div[2]/ul/li[1]/div[2]/div[1]/div[1]/h2/a/span")).Text;
                var result = new {cover, title, ISBN, authors};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                driver.Quit();
                var error = new {error = "Book not found"};
                return Ok(error);
            }
        }
    }
}