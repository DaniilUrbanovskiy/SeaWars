using Microsoft.AspNetCore.Mvc;
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

            var field = options.Field.ToDoubleDimension();           

            AttackStatus attackStatus = default;

            if (options.MovesCounter == 1)
            {
                DataStorage.EnemyField.MainField = field;
                attackStatus = Attack.AttackTheShip(DataStorage.EnemyField, options.StartPoint);
                field = DataStorage.EnemyField.MainField;
            }
            else
            {
                DataStorage.Field.MainField = field;
                attackStatus = Attack.AttackTheShip(DataStorage.Field, options.StartPoint);
                field = DataStorage.Field.MainField;
            }

            attackResponse.AttackStatus = attackStatus;
            attackResponse.Field = field.ToJaggedArray();

            var serialized = JsonConvert.SerializeObject(attackResponse);

            return Ok(serialized);
        }

        [HttpPost("SmartAttack")]
        public IActionResult SmartAttack([FromBody]AttackOptions options)
        {
            string startPoint = Attack.SmartAttack(options.Field.ToDoubleDimension(), options.StartPoint);

            var serialized = JsonConvert.SerializeObject(startPoint);
            return Ok(serialized);
        }
    }
}
