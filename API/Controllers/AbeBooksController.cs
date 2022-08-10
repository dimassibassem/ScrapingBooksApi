using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class AbeBooksController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            var infos = BllAbeBooks.GetInfoFromAbeBooks(isbn);
            return Ok(infos);
        }
    }
}