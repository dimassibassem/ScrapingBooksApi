using API.BLL;
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
            BLL_Home bll = new BLL_Home();
            var infos = bll.GetInfos(isbn);
            return Ok(infos);
        }
    }
}