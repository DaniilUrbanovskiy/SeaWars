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
    public class ConditionController : ControllerBase
    {
        [HttpGet("GetGameStatus/{whoseField}")]
        public IActionResult GetGameStatus([FromRoute] WhoseField whoseField)
        {
            bool isGameEnded = false;
            if (whoseField == WhoseField.EnemysField)
            {
                isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.EnemyField);
            }
            else
            {
                isGameEnded = AvaibilityValidation.IsGameEnded(DataStorage.Field);
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
 
    }
}