using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AmazonController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.amazon.ca/s?k=+" + isbn + "&crid=3P70C7NM9WVF7&sprefix=" + isbn +
                                          "%2Caps%2C190&ref=nb_sb_noss");
                var button = driver.FindElement(By.XPath(
                    "//*[@id='search']/div[1]/div[1]/div/span[3]/div[2]/div[2]/div/div/div/div/div[1]/span/a"));
                button.Click();
                System.Threading.Thread.Sleep(5000);
                var cover = driver
                    .FindElement(By.XPath(
                        "/html/body/div[2]/div[2]/div[3]/div[1]/div[6]/div[1]/div[1]/div[1]/div[1]/div/div/div/img"))
                    .GetAttribute("src");
                var title = driver.FindElement(By.XPath("//*[@id='productTitle']")).Text;
                var editor = driver
                    .FindElement(By.XPath("/html/body/div[2]/div[2]/div[3]/div[1]/div[6]/div[2]/div/div/div[1]/div[2]"))
                    .Text;
                var ISBN = driver
                    .FindElement(By.XPath("/html/body/div[2]/div[2]/div[3]/div[19]/div/div[1]/ul/li[5]/span/span[2]"))
                    .Text;
                var result = new {cover, editor, title, ISBN};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new
                {
                    error = "Book not found"
                };
                driver.Quit();
                return Ok(error);
            }
        }
    }
}