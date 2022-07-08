using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class BookFinderController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.bookfinder.com/search/?author=&title=&lang=en&isbn=" + isbn +
                                          "&new_used=*&destination=tn&currency=USD&mode=basic&st=sr&ac=qr");
                var cover = driver.FindElement(By.XPath("//*[@id='coverImage']")).GetAttribute("src");
                var editor = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[2]/p")).Text;
                editor = editor.Substring(3);
                var title = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[2]/a")).Text;
                var ISBN = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[1]/h1")).Text;
                ISBN = ISBN.Substring(0, 13);
                var result = new {cover, editor, title, ISBN};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                driver.Quit();
                return Ok("Book not found");
            }
        }
    }
}