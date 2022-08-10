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
            BllAmazon bll = new BllAmazon();
            var infos = bll.GetInfoFromAmazon(isbn);
            return Ok(infos);
        }
    }
}