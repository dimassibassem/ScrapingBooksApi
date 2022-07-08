using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.cpu.rnu.tn/index.php/shop/");
                var input = driver.FindElement(
                    By.XPath("//*[@id='woocommerce_product_search-2']/div/form/p[1]/input[1]"));
                input.SendKeys(isbn);
                
                System.Threading.Thread.Sleep(2000);
                var button =
                    driver.FindElement(By.XPath("//*[@id='woocommerce_product_search-2']/div/form/p[2]/button"));
                button.Click();

                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("here");
                var cover = driver.FindElement(By.ClassName("attachment-shop_single")).GetAttribute("src");
                var ISBN = driver.FindElement(By.XPath("//div[1]/div[2]/div[2]/div/p[3]"));
                var title = driver.FindElement(By.XPath("//div[1]/div[2]/div[1]/h1"));
                var editor = driver.FindElement(By.XPath("//div[1]/div[2]/div[2]/div/p/strong"));
                var result = new {cover, editor, title, ISBN};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                var error = new {error = "Book not found"};
                driver.Quit();
                return Ok(error);
            }
        }
    }
}