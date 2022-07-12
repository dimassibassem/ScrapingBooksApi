using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.BLL;

public class BLL_BookFinder
{
    public object GetInfoFromBookFinder(string isbn)
    {
        IWebDriver driver;
        driver = new ChromeDriver();
        try
        {
            driver.Navigate().GoToUrl("https://www.bookfinder.com/search/?author=&title=&lang=en&isbn=" + isbn +
                                      "&new_used=*&destination=tn&currency=USD&mode=basic&st=sr&ac=qr");
            var cover = driver.FindElement(By.XPath("//*[@id='coverImage']")).GetAttribute("src");
            var editorString = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[2]/p")).Text;
            editorString = editorString.Substring(3);
            var authors = editorString.Split(new[] {";", "-", "/"}, StringSplitOptions.RemoveEmptyEntries);
            var title = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[2]/a")).Text;
            var ISBN = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[2]/div[1]/h1")).Text;
            ISBN = ISBN.Substring(0, 13);
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception e)
        {
            var error = new
            {
                error = "Book not found"
            };
            driver.Quit();
            return error;
        }
    }
}