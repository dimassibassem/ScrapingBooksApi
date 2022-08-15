using API.Models.BLL;
using API.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewBntSearchController : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Post(string query)
        {
            var infos = await BllBntDatabase.GetBntBookByQuery(query);
            return Ok(infos);
        }
    }
}