using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]/{*title}")]
    [ApiController]
    public class BiruniByTitleController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string title)
        {
           BLL_BiruniDatbase bll = new BLL_BiruniDatbase();
              var result = bll.GetBiruniBooksByTitle(title);
                return Ok(result);
        }
    }
}