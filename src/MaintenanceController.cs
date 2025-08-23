// Maintenance verbs of the smoke service. This repository is a deliberately vulnerable
// smoke target for appsec-review; see README.md.
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Smoke
{
    [ApiController]
    [Route("maintenance")]
    public class MaintenanceController : ControllerBase
    {
        private const string LogRoot = "/var/log/smoke";

        // Rotates the server log named by the request through logrotate.
        private static bool RotateLogs(string name)
        {
            Process process = Process.Start("/bin/sh", "-c logrotate -f " + Path.Combine(LogRoot, name));
            process.WaitForExit();
            return process.ExitCode == 0;
        }

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
            else if (verb == "rotate" && arg != null)
            {
                status = RotateLogs(arg) ? 200 : 500;
            }
            return status == 200 ? Ok() : StatusCode(status);
        }
    }
}
