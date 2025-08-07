// Maintenance verbs of the smoke service. This repository is a deliberately vulnerable
// smoke target for appsec-review; see README.md.
using Microsoft.AspNetCore.Mvc;

namespace Smoke
{
    [ApiController]
    [Route("maintenance")]
    public class MaintenanceController : ControllerBase
    {
        // Dispatches one maintenance verb and returns an HTTP status.
        [HttpPost("ops")]
        public IActionResult Dispatch(string verb, string arg)
        {
            int status = 400;
            if (verb == null || verb.Length == 0)
            {
                return BadRequest();
            }
            if (verb == "ping")
            {
                status = 200;
            }
            return status == 200 ? Ok() : StatusCode(status);
        }
    }
}
