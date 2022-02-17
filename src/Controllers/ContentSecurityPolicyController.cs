using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace csplogger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentSecurityPolicyController : ControllerBase
    {
        private readonly ILogger<ContentSecurityPolicyController> _logger;

        public ContentSecurityPolicyController(ILogger<ContentSecurityPolicyController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CspData content)
        {
            _logger.LogWarning("CSP-Report: {@content}", content);
            return NoContent();
        }
        
        
    }
}