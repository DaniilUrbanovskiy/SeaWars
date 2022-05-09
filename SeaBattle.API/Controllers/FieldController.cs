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
    public class FieldController : ControllerBase
    {
        [HttpGet("{whoseField}/{gameId}")]
        public IActionResult CreateField([FromRoute]WhoseField whoseField, [FromRoute]int gameId)
        {
            Field field = new Field();
            field.MainField = FieldHandler.CreateField();
            if (whoseField == WhoseField.Field)
            {
                DataStorage.Games[gameId].Field.MainField = field.MainField;
            }
            else
            {
                DataStorage.Games[gameId].EnemyField.MainField = field.MainField;
            }
            var serialized = JsonConvert.SerializeObject(field.MainField);

            return Ok(serialized);
        }

        [HttpGet("GetEnemyField/{whoseField}/{gameId}")]
        public IActionResult GetEnemyField([FromRoute] WhoseField whoseField, [FromRoute] int gameId)
        {
            Field field = new Field();
            if (whoseField == WhoseField.Field)
            {
                field.MainField = DataStorage.Games[gameId].Field.MainField;
            }
            else
            {
                field.MainField = DataStorage.Games[gameId].EnemyField.MainField;
            }
            var serialized = JsonConvert.SerializeObject(field.MainField);

            return Ok(serialized);
        }

        [HttpGet("RandInit/{whoseField}/{gameId}")]
        public IActionResult RandInit([FromRoute]WhoseField whoseField, [FromRoute] int gameId)
        {
            Field field = whoseField == WhoseField.Field ? DataStorage.Games[gameId].Field : DataStorage.Games[gameId].EnemyField;

            field.MainField = FieldHandler.InicializeFieldByRandom(field.MainField);
            var serialized = JsonConvert.SerializeObject(field.MainField);

            return Ok(serialized);
        }

        [HttpPost("OwnInit/{whoseField}/{gameId}")]
        public IActionResult OwnInit([FromBody]ShipOtions shipOtions, [FromRoute]WhoseField whoseField, [FromRoute] int gameId)
        {
            Field field = whoseField == WhoseField.Field ? DataStorage.Games[gameId].Field : DataStorage.Games[gameId].EnemyField;

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
