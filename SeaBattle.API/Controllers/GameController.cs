using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SeaBattle.Application.Core;


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
            field.CreateField();

            var serialized = JsonConvert.SerializeObject(field.MainField);
            return Ok(serialized);
        }


    }
}
