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
    public class GameController : ControllerBase
    {
        Random random = new Random();

        [HttpGet("GetRandomPoint")]
        public IActionResult GetRandomPoint()
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
            DataStorage.GameId = gameId;
            return Ok(gameId);
        }

        [HttpPost("ConnectToGame")]
        public IActionResult ConnectToGame([FromBody]int idGame)
        {
            string message = String.Empty;
            if (DataStorage.GameId == idGame)
            {
                message = "Connected!";
                DataStorage.IsConnected = true;
            }
            else
            {
                message = "Try again!";
                DataStorage.IsConnected = false;
            }
            var serialized = JsonConvert.SerializeObject(message);
            return Ok(serialized);
        }

        [HttpGet("GetConnectionStatus")]
        public IActionResult GetConnectionStatus()
        {
            bool isConected = DataStorage.IsConnected;
            var serialized = JsonConvert.SerializeObject(isConected);
            return Ok(serialized);
        }
    }      
}