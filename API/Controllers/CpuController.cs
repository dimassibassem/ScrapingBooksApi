using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            var infos = BllCpu.GetInfoFromCpu(isbn);
            return Ok(infos);
        }
    }
}