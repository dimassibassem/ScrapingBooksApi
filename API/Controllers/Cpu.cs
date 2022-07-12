﻿using API.BLL;
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
            BLL_Cpu bll = new BLL_Cpu();
            var infos = bll.GetInfoFromCpu(isbn);
            return Ok(infos);
        }
    }
}