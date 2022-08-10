using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace API.Models.BLL;

public static class BllBnt
{
    public static object GetInfoFromBnt(string isbn)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://www.bnt.nat.tn");
        Thread.Sleep(2000);
        var input = driver.FindElement(By.Id("searchdata1"));
        input.SendKeys(isbn);
        Thread.Sleep(2000);
        var dropdown = driver.FindElement(By.Id("srchfield1"));
        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByText("ISBN");
        Thread.Sleep(1000);
        var button =
            driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div[1]/div[1]/div/form[1]/div/div[2]/input"));
        button.Click();
        Thread.Sleep(2000);

        try
        {
            driver.FindElement(By.XPath("//*[@id='hitlist']/ul/li[3]/ul[1]/li[3]/dl/dd[1]/a")).Click();
            Thread.Sleep(2000);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            var cover = driver.FindElement(By.XPath("/html/body/div[5]/div[1]/form[1]/div[2]/div[1]/ul[1]/img"))
                .GetAttribute("src");
            var ISBN = driver.FindElement(By.ClassName("isbn")).Text;
            var authors = driver.FindElement(By.ClassName("author")).Text;
            var title = driver
                .FindElement(By.ClassName("title")).Text;
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception)
        {
            driver.Quit();
            var error = new {error = "Book not found"};
            return error;
        }
    }

    public static object GetInfoFromBntWithTitle(string typedTitle)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl("https://www.bnt.nat.tn");
        Thread.Sleep(2000);
        var input = driver.FindElement(By.Id("searchdata1"));
        input.SendKeys(typedTitle);
        Thread.Sleep(2000);
        var dropdown = driver.FindElement(By.Id("srchfield1"));
        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByText("Titre");
        Thread.Sleep(1000);
        var button =
            driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div[1]/div[1]/div/form[1]/div/div[2]/input"));
        button.Click();
        Thread.Sleep(2000);

        try
        {
            driver.FindElement(By.XPath("//*[@id='hitlist']/ul/li[3]/ul[1]/li[3]/dl/dd[1]/a")).Click();
            Thread.Sleep(2000);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            var cover = driver.FindElement(By.XPath("/html/body/div[5]/div[1]/form[1]/div[2]/div[1]/ul[1]/img"))
                .GetAttribute("src");
            var ISBN = driver.FindElement(By.ClassName("isbn")) != null
                ? driver.FindElement(By.ClassName("isbn")).Text
                : "";
            var authors = driver.FindElement(By.ClassName("author")).Text;
            var title = driver
                .FindElement(By.ClassName("title")).Text;
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            driver.Quit();
            var error = new {error = "Book not found"};
            return error;
        }
    }
}