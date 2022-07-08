﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AlmanhalController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://platform.almanhal.com/Search/Result?q=&sf_28_0_2=" + isbn +
                                          "&opsf_28_0=1");
                System.Threading.Thread.Sleep(5000);
                var links = driver.FindElements(By.ClassName("btn-title-item-action"));
                var link = links[1].GetAttribute("href");
                driver.Navigate().GoToUrl(link);
                System.Threading.Thread.Sleep(5000);
                var cover = driver
                    .FindElement(By.XPath(
                        "/html/body/div[2]/div[5]/main/div/div/div/aside/div/div[1]/figure/a/div/img"))
                    .GetAttribute("src");
                var title = driver
                    .FindElement(By.XPath("/html/body/div[2]/div[5]/main/div/div/div/div/section[1]/h5/a")).Text;
                var editor = driver
                    .FindElement(By.XPath(
                        "/html/body/div[2]/div[5]/main/div/div/div/div/section[2]/div/div/div/div[1]/div[2]/div/label/a[2]"))
                    .Text;
                var ISBN = driver
                    .FindElement(By.XPath(
                        "/html/body/div[2]/div[5]/main/div/div/div/div/section[2]/section/div/div[1]/div[6]/div/div[2]/label/a"))
                    .Text;
                var result = new {cover, editor, title, ISBN};
                driver.Quit();
                return Ok(result);
            }
            catch (Exception e)
            {
                driver.Quit();
                var error = new
                {
                    error = "Book not found",
                };
                return Ok(error);
            }
        }
    }
}