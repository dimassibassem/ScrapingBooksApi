using System.Collections;
using API.Models.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Models.BLL;

public class BLL_BiruniDatbase
{
    public static void GetBiruniDatabase()
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
            var pagination = driver.FindElement(By.Id("NB_ITEM_PER_PAGE_INPUT_hidden"));
            Thread.Sleep(3000);
            // set pagination value to 50
            // if we set it superior than 50, the server will return an error of timeout
            // I set to 10 cause of internet issues
            js.ExecuteScript("arguments[0].value = 10", pagination);
            button.Click();
            Thread.Sleep(8000);


            //Adding a loop over pages

            var iterate = js.ExecuteScript(
                @"var string  = document.querySelector('#div-list-item-results > div > table.RGrid.lower-table > thead > tr > th:nth-child(2) > table > tbody > tr > td:nth-child(2) > a').textContent ; 
                var val = string.slice(string.indexOf('/')+2) ; 
                val = parseInt(val);
                return val ;
                 ");

            for (var i = 1; i < (long) iterate; i++)
            {
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


                var writableCollection = (JArray) JToken.FromObject(detailsObject);

                foreach (var detail in writableCollection)
                {
                    var biruniBook = new BiruniBook
                    {
                        Title = detail["title"]?.ToString(),
                        Author = detail["auteurs"]?.ToString(),
                        Edition = detail["edition"]?.ToString(),
                        ISBN = detail["isbn"]?.ToString(),
                        Collection = detail["collection"]?.ToString(),
                        Cover = detail["cover"]?.ToString()
                    };
                    if (biruniBook.Title == null) continue;
                    var isExist = DALBiruniBook.GetBiruniBook(biruniBook.Title);
                    if (isExist.Id == null)
                    {
                        DALBiruniBook.InsertBook(biruniBook);
                    }
                }
                
                // next page
                js.ExecuteScript("rechercheDocument_next('form_haut_get_recherche_next')");
                Thread.Sleep(5000);
            }
            
            driver.Quit();
        }
        catch (Exception e)
        {
            driver.Quit();
            Console.WriteLine(e.Message);
        }
       
    }
}