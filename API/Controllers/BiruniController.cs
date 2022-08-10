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
            var result = BLL_BiruniDatbase.GetBiruniBooksByTitle(title);
            return Ok(result);
        }
    }
}