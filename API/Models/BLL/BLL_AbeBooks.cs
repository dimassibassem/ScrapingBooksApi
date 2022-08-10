using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Models.BLL;

public static class BllAbeBooks
{
    public static object GetInfoFromAbeBooks(string isbn)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);
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
            return result;
        }
        catch (Exception)
        {
            driver.Quit();
            var error = new {error = "Book not found"};
            return error;
        }
    }
}