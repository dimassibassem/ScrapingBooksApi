using System.Collections;
using API.Models.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Models.BLL;

public class BLL_BiruniDatbase
{
    public static object? GetBiruniDatabase()
    {
        ChromeOptions options = new ChromeOptions();
        // options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);
        IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
        try
        {
            driver.Navigate().GoToUrl("http://www.biruni.tn/catalogue-universitaire.php");
            Thread.Sleep(5000);
            var button = driver.FindElement(By.XPath(
                "/html/body/ul/li[4]/div/div/div/table/tbody[4]/tr/td/ul/li/div/div/div[2]/ul/li/div/div[1]/ul/li/form/table/tbody[2]/tr/td[2]/div/div/div/div"));
            var paginations = driver.FindElement(By.Id("NB_ITEM_PER_PAGE_INPUT_hidden"));
            Thread.Sleep(3000);
            // set pagination value to 100
            // if we set it superior than 100, the server will return an error of timeout
            js.ExecuteScript("arguments[0].value = 10", paginations);
            button.Click();
            Thread.Sleep(8000);

            var detailsObject = js.ExecuteScript(@" let array = [];
        let tbodies = document.getElementsByClassName('tbody');
        for (let i = 0; i < tbodies.length; i++) {
            let obj = {title: '', auteurs: '', edition: '',isbn:'',collection:'',cover:''};
            let title = tbodies[i].querySelector(' tr > td:nth-child(2) > div > table > tbody > tr > td:nth-child(2) > div > h2').innerText;
           let author = tbodies[i].querySelector(' tr > td:nth-child(2) > div > table > tbody > tr > td:nth-child(2) > div > div').innerText;
            let edition = tbodies[i].querySelector(' tr > td:nth-child(2) > div > table > tbody > tr > td:nth-child(2) > div > div:nth-child(3)').innerText;
            obj.cover = tbodies[i].querySelector('tr > td:nth-child(1) > div > img').src === 'http://www.bu.turen.tn/media/mini/0.jpg' ? '' : tbodies[i].querySelector('tr > td:nth-child(1) > div > img').src;
            let detailsArray = edition.split('|');
            obj.title = title.split(':')[1].trim();
            obj.auteurs = author.split(':')[1].trim();
            detailsArray.forEach(detail => {
                if(detail.includes('ISBN')){
                    obj.isbn = detail.split(':')[1].trim();
                }
                if(detail.includes('Collection')){
                    obj.collection = detail.split(':')[1].trim();
                }
                if (detail.includes('Edtion')) {
                    obj.edition = detail.slice(detail.indexOf(':') + 1).trim();
                }
            })
            array.push(obj);
        }
        return array");


            // cast readOnlyCollection to WritableCollection
            var writableCollection = (JArray) JToken.FromObject(detailsObject);

// Console.WriteLine(writableCollection[0]["title"]);
            for (var i = 0; i < writableCollection.Count; i++)
            {
                var biruniBook = new BiruniBook
                    {
                        Title = writableCollection[i]["title"]?.ToString(),
                        Author = writableCollection[i]["auteurs"]?.ToString(),
                        Edition = writableCollection[i]["edition"]?.ToString(),
                        ISBN = writableCollection[i]["isbn"]?.ToString(),
                        Collection = writableCollection[i]["collection"]?.ToString(),
                        Cover = writableCollection[i]["cover"]?.ToString()
                    };
                    Console.WriteLine("Book " + i + ": " + biruniBook);
                    DALBiruniBook.InsertBook(biruniBook);
            }
            
            
            


            driver.Quit();


            return writableCollection;
        }
        catch (Exception e)
        {
            driver.Quit();
            Console.WriteLine(e.Message);
        }

        return null;
    }
}