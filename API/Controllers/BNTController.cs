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
    public class BNTController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string isbn)
        {
            BllBnt bll = new BllBnt();
            var infos = bll.GetInfoFromBNT(isbn);
            return Ok(infos);
        }
    }

    [Route("api/BNT/title/{*title}")]
    [ApiController]
    public class BNT_with_title : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string title)
        {
            BllBnt bll = new BllBnt();
            var infos = bll.GetInfoFromBNTWithTitle(title);
            return Ok(infos);
        }
    }
}