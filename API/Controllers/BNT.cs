using API.BLL;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class BNTController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BLL_BNT bll = new BLL_BNT();
            var infos = bll.GetInfoFromBNT(isbn);
            return Ok(infos);
        }
    }
}