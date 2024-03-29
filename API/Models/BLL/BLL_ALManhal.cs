using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Models.BLL;

public static class BllAlManhal
{
    public static object GetInfoFromAlManhal(string isbn)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);
        try
        {
            driver.Navigate().GoToUrl("https://platform.almanhal.com/Search/Result?q=&sf_28_0_2=" + isbn +
                                      "&opsf_28_0=1");
            Thread.Sleep(5000);
            var links = driver.FindElements(By.ClassName("btn-title-item-action"));
            var link = links[1].GetAttribute("href");
            driver.Navigate().GoToUrl(link);
            Thread.Sleep(5000);
            var cover = driver
                .FindElement(By.XPath(
                    "/html/body/div[2]/div[5]/main/div/div/div/aside/div/div[1]/figure/a/div/img"))
                .GetAttribute("src");
            var title = driver
                .FindElement(By.XPath("/html/body/div[2]/div[5]/main/div/div/div/div/section[1]/h5/a")).Text;
            var editorString = driver
                .FindElement(By.XPath(
                    "/html/body/div[2]/div[5]/main/div/div/div/div/section[2]/div/div/div/div[1]/div[2]/div"))
                .Text;
            var authors = editorString.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
            var ISBN = driver
                .FindElement(By.XPath(
                    "/html/body/div[2]/div[5]/main/div/div/div/div/section[2]/section/div/div[1]/div[6]/div/div[2]/label/a"))
                .Text;
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception)
        {
            driver.Quit();
            var error = new
                {error = "Book not found"};
            return error;
        }
    }
}