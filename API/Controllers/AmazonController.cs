using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AmazonController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            var infos = BllAmazon.GetInfoFromAmazon(isbn);
            return Ok(infos);
        }
    }
}