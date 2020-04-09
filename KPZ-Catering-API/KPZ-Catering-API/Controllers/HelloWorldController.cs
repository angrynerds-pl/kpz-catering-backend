using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPZ_Catering_API.Controllers
{
    [Route("")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet("")]
        public string getHelloWorld() {
            return "Hello World!";
        }
    }
}