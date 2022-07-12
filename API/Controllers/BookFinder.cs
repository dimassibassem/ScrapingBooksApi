using API.BLL;
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
            BLL_BookFinder bll = new BLL_BookFinder();
            var infos = bll.GetInfoFromBookFinder(isbn);
            return Ok(infos);
        }
    }
}