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
        [HttpPost("SetPoint/{gameId}")]
        public IActionResult SetPoint([FromBody] AttackOptions options, [FromRoute] int gameId)
        {
            AttackResponse attackResponse = new AttackResponse();

            var field = options.Field.ToDoubleDimension();
            AttackStatus attackStatus = default;
            if (options.MovesCounter == 1)
            {
                DataStorage.Games[gameId].EnemyField.MainField = field;
                attackStatus = Attack.AttackTheShip(DataStorage.Games[gameId].EnemyField, options.StartPoint);
                field = DataStorage.Games[gameId].EnemyField.MainField;
            }
            else
            {
                DataStorage.Games[gameId].Field.MainField = field;
                attackStatus = Attack.AttackTheShip(DataStorage.Games[gameId].Field, options.StartPoint);
                field = DataStorage.Games[gameId].Field.MainField;
            }
            attackResponse.AttackStatus = attackStatus;
            attackResponse.Field = field.ToJaggedArray();
            var serialized = JsonConvert.SerializeObject(attackResponse);

            return Ok(serialized);
        }

        [HttpPost("SmartAttack")]
        public IActionResult SmartAttack([FromBody] AttackOptions options)
        {
            string startPoint = Attack.SmartAttack(options.Field.ToDoubleDimension(), options.StartPoint);
            var serialized = JsonConvert.SerializeObject(startPoint);

            return Ok(serialized);
        }

        [HttpPost("SetAttackCondition/{userChoice}/{gameId}")]
        public IActionResult SetAttackCondition([FromRoute]int userChoice, [FromRoute]int gameId)
        {
            if (userChoice == 1)
            {
                DataStorage.Games[gameId].IsFirstUserAttackFinished = true;
            }
            else
            {
                DataStorage.Games[gameId].IsSecondUserAttackFinished = true;
            }
            return Ok();
        }

        [HttpGet("GetAttackCondition/{userChoice}/{gameId}")]
        public IActionResult GetAttackCondition([FromRoute] int userChoice, [FromRoute] int gameId)
        {
            if (userChoice == 1)
            {
                return Ok(DataStorage.Games[gameId].IsSecondUserAttackFinished);
            }
            return Ok(DataStorage.Games[gameId].IsSecondUserAttackFinished);
        }





    }
}
