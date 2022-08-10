using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class BookFinderController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BllBookFinder bll = new BllBookFinder();
            var infos = bll.GetInfoFromBookFinder(isbn);
            return Ok(infos);
        }
    }
}