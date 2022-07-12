using API.BLL;
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
            BLL_Amazon bll = new BLL_Amazon();
            var infos = bll.GetInfoFromAmazon(isbn);
            return Ok(infos);
        }
    }
}