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
    public class AttackController : ControllerBase
    {
        [HttpPost("SetPoint")]
        public IActionResult SetPoint([FromBody]AttackOptions options)
        {
            AttackResponse attackResponse = new AttackResponse();

            var field = options.EnemyFieldHiden.ToDoubleDimension();           

            AttackStatus attackStatus = default;

            if (options.MovesCounter == 1)
            {
                attackStatus = Attack.AttackTheShip(DataStorage.EnemyField, ref field, options.StartPoint, options.MovesCounter);
            }
            else
            {
                DataStorage.Field.MainField = field;
                attackStatus = Attack.AttackTheShip(DataStorage.Field, ref field, options.StartPoint, options.MovesCounter);
                field = DataStorage.Field.MainField;
            }

            attackResponse.AttackStatus = attackStatus;
            attackResponse.EnemyFieldHiden = field.ToJaggedArray();

            var serialized = JsonConvert.SerializeObject(attackResponse);

            return Ok(serialized);
        }

        [HttpPost("SmartAttack")]
        public IActionResult SmartAttack([FromBody]AttackOptions options)
        {
            string startPoint = Attack.SmartAttack(options.EnemyFieldHiden.ToDoubleDimension(), options.StartPoint);

            var serialized = JsonConvert.SerializeObject(startPoint);
            return Ok(serialized);
        }
    }
}
