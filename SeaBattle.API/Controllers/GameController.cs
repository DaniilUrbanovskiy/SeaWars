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
            Game currentGame = new Game();
            currentGame.GameId = gameId;
            DataStorage.Games.Add(currentGame.GameId, currentGame);

            return Ok(gameId);
        }

        [HttpPost("ConnectToGame/{gameId}")]
        public IActionResult ConnectToGame([FromBody]int enteredGameId, [FromRoute]int gameId)
        {
            string message = String.Empty;
            if (gameId == enteredGameId)
            {
                message = "Connected!";
                DataStorage.Games[gameId].IsConnected = true;
            }
            else
            {
                message = "Try again!";
                DataStorage.Games[gameId].IsConnected = false;
            }
            var serialized = JsonConvert.SerializeObject(message);

            return Ok(serialized);
        }

        [HttpGet("GetConnectionStatus/{gameId}")]
        public IActionResult GetConnectionStatus([FromRoute]int gameId)
        {
            bool isConected = DataStorage.Games[gameId].IsConnected;
            var serialized = JsonConvert.SerializeObject(isConected);

            return Ok(serialized);
        }
    }      
}