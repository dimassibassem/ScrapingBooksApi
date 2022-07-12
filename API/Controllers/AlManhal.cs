using API.BLL;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AlmanhalController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BLL_ALManhal bll = new BLL_ALManhal();
            var infos = bll.GetInfoFromALManhal(isbn);
            return Ok(infos);
        }
    }
}