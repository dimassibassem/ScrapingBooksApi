using API.Models.BLL;
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
            BllAbeBooks bll = new BllAbeBooks();
            var infos = bll.GetInfoFromAbeBooks(isbn);
            return Ok(infos);
        }
    }
}