using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class CairnLoginAndScrapeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            
            // still getting errors with this 
            // even I try to use the driver with free proxy, it still doesn't work
            ChromeOptions options = new ChromeOptions();
            
            var PROXY_STR = "198.59.191.234:8080";

            options.AddArgument("--proxy-server=" + PROXY_STR);
            
            IWebDriver driver;
            driver = new ChromeDriver(options);
            // driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.cairn.info/");
                var connexionButton =
                    driver.FindElement(By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[2]/a"));
                connexionButton.Click();
                System.Threading.Thread.Sleep(1000);
                var emailInput = driver.FindElement(
                    By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[2]/div/div/div[1]/form/div[1]/input"));
                emailInput.SendKeys("dimassibassem99@gmail.com");

                System.Threading.Thread.Sleep(2000);
                var passwordInput = driver.FindElement(
                    By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[2]/div/div/div[1]/form/div[2]/input"));
                passwordInput.SendKeys("test123456789");
                var button =
                    driver.FindElement(
                        By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[2]/div/div/div[1]/form/div[4]/input"));
                button.Click();

                System.Threading.Thread.Sleep(15000);

                var searchInput =
                    driver.FindElement(
                        By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[1]/div/form/div[1]/input"));

                searchInput.SendKeys(isbn);
                System.Threading.Thread.Sleep(2000);
                var searchButton =
                    driver.FindElement(By.XPath("/html/body/header[1]/div[1]/div/div/nav/ul/li[1]/div/form/div[1]/a"));
                searchButton.Click();
                System.Threading.Thread.Sleep(5000);

                var link = driver
                    .FindElement(
                        By.XPath("/html/body/main/section[2]/div/div[2]/div/div[2]/div[2]/div/div[1]/ul/li[1]/a"))
                    .GetAttribute("href");
                driver.Navigate().GoToUrl(link);
                System.Threading.Thread.Sleep(5000);

                var cover = driver.FindElement(By.XPath("/html/body/main/section[2]/div/div/div/div[1]/img"))
                    .GetAttribute("src");
                var ISBN = driver.FindElement(By.XPath("/html/body/main/section[5]/div/div/div/div/dl/dd[2]/text()[1]"))
                    .Text;
                var title = driver
                    .FindElement(By.XPath("/html/body/main/section[2]/div/div/div/div[2]/div[1]/ul[1]/li[1]")).Text;
                var editor = driver
                    .FindElement(By.XPath("/html/body/main/section[2]/div/div/div/div[2]/div[1]/ul[4]/li[4]/b/a")).Text;
                var result = new {cover, editor, title, ISBN};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var error = new {error = "Book not found"};
                driver.Quit();
                return Ok(error);
            }
        }
    }
}