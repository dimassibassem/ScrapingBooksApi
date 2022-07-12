using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.BLL;

public class BLL_Cpu
{
    public static string Reverse(string str)
    {
        char[] ch = str.ToCharArray();
        Array.Reverse(ch);
        return new string(ch);
    }

    public object GetInfoFromCpu(string isbn)
    {
        IWebDriver driver;
        driver = new ChromeDriver();
        IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
        try
        {
            isbn = isbn.Replace(" ", "");
            if (isbn.All(char.IsDigit))
            {
                if (isbn.Length < 13)
                {
                    var isbnError = new {error = "ISBN is not valid"};
                    return isbnError;
                }

                if (isbn.Length == 13)
                {
                    isbn = isbn.Insert(3, "-");
                    isbn = isbn.Insert(8, "-");
                    isbn = isbn.Insert(11, "-");
                    isbn = isbn.Insert(15, "-");
                }
            }


            driver.Navigate().GoToUrl("https://www.cpu.rnu.tn/index.php/shop/");
            var input = driver.FindElement(
                By.XPath("//*[@id='woocommerce_product_search-2']/div/form/p[1]/input[1]"));
            input.SendKeys(isbn);

            System.Threading.Thread.Sleep(2000);
            var button =
                driver.FindElement(By.XPath("//*[@id='woocommerce_product_search-2']/div/form/p[2]/button"));
            button.Click();

            System.Threading.Thread.Sleep(5000);
            var cover = driver.FindElement(By.ClassName("attachment-shop_single")).GetAttribute("src");
            var ISBN = js.ExecuteScript("const ps = document.querySelectorAll('p');" +
                                        "for (var i = 0; i < ps.length; i++) {" +
                                        "if (ps[i].textContent.includes('ت.د.م.ك')) {" +
                                        "return ps[i].textContent" +
                                        "}" +
                                        "if(ps[i].textContent.includes('ISBN'))return(ps[i].textContent);" +
                                        "}");

            if (ISBN.ToString().Contains("ت.د.م.ك"))
            {
                ISBN = ISBN.ToString().Replace("ت.د.م.ك. : ", "");
                ISBN = Reverse(ISBN.ToString());
            }

            ISBN = ISBN.ToString().Replace("ISBN : ", "");
            var title = driver.FindElement(By.XPath("//div[1]/div[2]/div[1]/h1")).Text;
            var editorString = js.ExecuteScript(
                "return (document.querySelector('div.cmsmasters_single_product > div.summary.entry-summary.cmsmasters_product_right_column > div.cmsmasters_product_content > div > p > strong') != null ?  (document.querySelector('div.cmsmasters_single_product > div.summary.entry-summary.cmsmasters_product_right_column > div.cmsmasters_product_content > div > p > strong').textContent.includes(':') ? document.querySelector('div.cmsmasters_single_product > div.summary.entry-summary.cmsmasters_product_right_column > div.cmsmasters_product_content > div > p:nth-child(2) > strong').textContent : document.querySelector('div.cmsmasters_single_product > div.summary.entry-summary.cmsmasters_product_right_column > div.cmsmasters_product_content > div > p > strong').textContent) : document.querySelector('div.cmsmasters_single_product > div.summary.entry-summary.cmsmasters_product_right_column > div.cmsmasters_product_content > div > p:nth-child(2)').textContent)");
            var authors = editorString.ToString().Split(new[] {",", "et"}, StringSplitOptions.RemoveEmptyEntries);
            var result = new {cover, title, ISBN, authors};
            driver.Quit();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            var error = new {error = "Book not found"};
            driver.Quit();
            return error;
        }
    }
}