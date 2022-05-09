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
using SeaBattle.Infrastructure.Extentions;

namespace SeaBattle.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet("GetGameStatus/{whoseField}/{gameId}")]
        public IActionResult GetGameStatus([FromRoute] WhoseField whoseField, [FromRoute]int gameId)
        {
            bool isGameEnded = false;
            if (whoseField == WhoseField.EnemysField)
            {
                //isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.EnemyField);

                isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.Games[gameId].EnemyField);
            }
            else
            {
                //isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.Field);

                isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.Games[gameId].Field);
            }
            if (isGameEnded == true)
            {
                //DataStorage.IsEnded = true;

                DataStorage.Games.Remove(gameId);
            }
            var serialized = JsonConvert.SerializeObject(isGameEnded);
            return Ok(serialized);
        }

        [HttpPost("GetShipStatus")]
        public IActionResult GetShipStatus([FromBody] string[][] field)
        {
            string hittedShip = AvaibilityValidation.IsHittedShip(field.ToDoubleDimension());           
            var serialized = JsonConvert.SerializeObject(hittedShip);

            return Ok(serialized);
        }

        [HttpPost("SetReadyStatus/{whoseField}/{gameId}")]
        public IActionResult SetReadyStatus([FromRoute]WhoseField whoseField, [FromRoute] int gameId)
        {
            if (whoseField == WhoseField.Field)
            {
                DataStorage.Games[gameId].Field.IsCreated = true;
            }
            else
            {
                DataStorage.Games[gameId].EnemyField.IsCreated = true;
            }

            return Ok();
        }

        [HttpGet("GetReadyStatus/{whoseField/{gameId}}")]
        public IActionResult GetReadyStatus([FromRoute] WhoseField whoseField, [FromRoute] int gameId)
        {
            return Ok(whoseField == WhoseField.Field ? DataStorage.Games[gameId].Field.IsCreated : DataStorage.Games[gameId].EnemyField.IsCreated);
        }

    }
}