using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeaBattle.Application.Core;
using SeaBattle.Application.DataAccess;
using SeaBattle.Infrastructure.Domain;
using SeaBattle.Infrastructure.Common;
using SeaBattle.Infrastructure.Extentions;

namespace SeaBattle.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : ControllerBase
    {
        Random random = new Random();

        [HttpGet("GetRandomPoint")]
        public IActionResult GetGameStatus([FromRoute] WhoseField whoseField)
        {            
            int num = random.Next(1, 11);
            char let = new char().GetRandomLetter();
            string startPoint = $"{Convert.ToString(num) + let}";

            var serialized = JsonConvert.SerializeObject(startPoint);
            return Ok(serialized);
        }

        [HttpGet("GetGameId")]
        public IActionResult GetGameId()
        {
            int gameId = random.Next(100);
            return Ok(gameId);
        }
    }      
}