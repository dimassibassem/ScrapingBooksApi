using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace API.BLL;

public class BLL_BNT
{
    public object GetInfoFromBNT(string isbn)
    {
        IWebDriver driver;
        ChromeOptions options = new ChromeOptions();
        // options.AddArgument("--headless");
        driver = new ChromeDriver(options);
        try
        {
            driver.Navigate().GoToUrl("https://www.bnt.nat.tn");
            Thread.Sleep(2000);
            var input = driver.FindElement(By.Id("searchdata1"));
            input.SendKeys(isbn);
            Thread.Sleep(2000);
            var dropdown = driver.FindElement(By.Id("srchfield1"));
            var selectElement = new SelectElement(dropdown);
            selectElement.SelectByText("ISBN");
            Thread.Sleep(1000);
            var button = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div[1]/div[1]/div/form[1]/div/div[2]/input"));
            button.Click();
            Thread.Sleep(2000);
            
            var cover = driver.FindElement(By.XPath("/html/body/div[5]/div[1]/form[1]/div[2]/div[1]/ul[1]/img")).GetAttribute("src");
            var ISBN = driver.FindElement(By.ClassName("isbn")).Text;
            var authors = driver.FindElement(By.ClassName("author")).Text;
            var title = driver
                .FindElement(By.ClassName("title")).Text;
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception e)
        {
            driver.Quit();
            var error = new {error = "Book not found"};
            return error;
        }
    }
}