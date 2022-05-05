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

namespace SeaBattle.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConditionController : ControllerBase
    {
        [HttpGet("GetGameStatus/{whoseField}")]
        public IActionResult GetGameStatus([FromRoute]WhoseField whoseField)
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

        [HttpPost("OwnInit")]
        public IActionResult OwnInit([FromBody]ShipOtions shipOtions)
        {
            Field field = DataStorage.Field;
            bool isHorizontal = shipOtions.Direction == Direction.Horizontal ? true : false;
            bool isPuted = FieldHandler.InicializeFieldByYourself(field.MainField, shipOtions.Point, shipOtions.DecksCount, isHorizontal);
            var serialized = JsonConvert.SerializeObject(field.MainField);
            if (isPuted == true)
            {
                return Ok(serialized);
            }
            else
            {
                return BadRequest(serialized);
            }
        }
    }
}