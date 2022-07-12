using API.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AbeBooksController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BLL_AbeBooks bll = new BLL_AbeBooks();
            var infos = bll.GetInfoFromAbeBooks(isbn);
            return Ok(infos);
        }
    }
}