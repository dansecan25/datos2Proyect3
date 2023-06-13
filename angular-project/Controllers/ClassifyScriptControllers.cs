using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassifyScriptController : ControllerBase
    {
        private readonly ILogger<ClassifyScriptController> _logger;

        public ClassifyScriptController(ILogger<ClassifyScriptController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "CreateXml")]
        public IActionResult ModifyXml()
        {
            return Ok();
        }
    }
}

