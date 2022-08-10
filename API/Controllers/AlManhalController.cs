using API.Models.BLL;
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
            BllAlManhal bll = new BllAlManhal();
            var infos = bll.GetInfoFromALManhal(isbn);
            return Ok(infos);
        }
    }
}