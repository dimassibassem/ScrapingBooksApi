using API.Models.BLL;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers

// I don't recommend using this controller
// the "BNT" website used in this controller
// is not the best source of information 
// I find out that the search in the website is not very accurate 
// and it have a lot of errors in the search
// especially in the search with ISBN number


{
    [Route("api/[controller]/{*isbn}")]
    [ApiController]
    public class BntController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            var infos = BllBnt.GetInfoFromBnt(isbn);
            return Ok(infos);
        }
    }

    [Route("api/BNT/title/{*title}")]
    [ApiController]
    public class BntWithTitle : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string title)
        {
            var infos = BllBnt.GetInfoFromBntWithTitle(title);
            return Ok(infos);
        }
    }
}