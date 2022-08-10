using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BllHome bll = new BllHome();
            var infos = bll.GetInfos(isbn);
            return Ok(infos);
        }
    }
}