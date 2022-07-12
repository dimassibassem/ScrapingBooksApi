using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.BLL;

public class BLL_Amazon
{
    public object GetInfoFromAmazon(string isbn)
    {
        IWebDriver driver;
        driver = new ChromeDriver();
        IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
        try
        {
            driver.Navigate().GoToUrl("https://www.amazon.ca/s?k=+" + isbn + "&crid=3P70C7NM9WVF7&sprefix=" + isbn +
                                      "%2Caps%2C190&ref=nb_sb_noss");
            var button = driver.FindElement(By.XPath(
                "//*[@id='search']/div[1]/div[1]/div/span[3]/div[2]/div[2]/div/div/div/div/div[1]/span/a"));
            System.Threading.Thread.Sleep(3000);
            button.Click();
            System.Threading.Thread.Sleep(5000);


            var cover = driver
                .FindElement(By.XPath(
                    "//*[@id='imgBlkFront']"))
                .GetAttribute("src");
            var title = driver.FindElement(By.XPath("//*[@id='productTitle']")).Text;
            var editorsElement = driver
                .FindElements(By.ClassName("_about-the-author-card_carouselItemStyles_authorName__HSb1t"));
            var editor = new List<string>();
            foreach (var elem in editorsElement)
            {
                editor.Add(elem.Text);
            }

            var ISBN = js.ExecuteScript("const div = document.querySelector('#detailBullets_feature_div > ul');" +
                                        "const lis = div.querySelectorAll('ul>li');" +
                                        "for (let i = 0; i < lis.length; i++)" +
                                        " {if(lis[i].querySelector('li>span>span').textContent.includes('ISBN-13')) return(lis[i].querySelector('li> span > span:nth-child(2)').textContent);}");

            System.Threading.Thread.Sleep(5000);
            var result = new {cover, title, ISBN, editor};
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