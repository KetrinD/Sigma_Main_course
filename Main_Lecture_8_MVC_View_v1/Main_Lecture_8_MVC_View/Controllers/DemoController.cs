using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Demo.Controllers
{
    //  [Route("DemoRoute")]
    public class DemoController : ControllerBase
    {
        public string GetHello()
        {
            return "Hello";
        }

        public int GetInt()
        {
            return 10;  
        }
    }
}
