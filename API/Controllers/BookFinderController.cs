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
            var infos = BllBookFinder.GetInfoFromBookFinder(isbn);
            return Ok(infos);
        }
    }
}