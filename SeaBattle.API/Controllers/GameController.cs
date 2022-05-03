using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeaBattle.Application.Core;
using SeaBattle.Application.DataAccess;
using SeaBattle.Application.Domain;

namespace SeaBattle.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FieldController : ControllerBase
    {
       [HttpGet]
        public IActionResult CreateField()
        {
            Field field = new Field();
            field.MainField = FieldHandler.CreateField();
            DataStorage.Field = field;
            var serialized = JsonConvert.SerializeObject(field.MainField);
            return Ok(serialized);
        }

        [HttpGet("RandInit/{whoseField}")]
        public IActionResult RandInit([FromRoute]WhoseField whoseField)
        {
            Field field = whoseField == WhoseField.Field ? DataStorage.Field : DataStorage.EnemyField;
            field.MainField = FieldHandler.InicializeFieldByRandom(field.MainField);

            var serialized = JsonConvert.SerializeObject(field.MainField);
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
                return BadRequest();
            }
        }
    }
}
