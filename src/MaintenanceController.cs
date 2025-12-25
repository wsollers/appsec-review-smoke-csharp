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
        private const string ReportRoot = "/var/lib/smoke/reports";
        private static readonly string[] Hooks = { "ping", "rotate", "report" };
        private static int hits;

        // Rotates the server log named by the request through logrotate.
        private static bool RotateLogs(string name)
        {
            Process process = Process.Start("/bin/sh", "-c logrotate -f " + Path.Combine(LogRoot, name));
            process.WaitForExit();
            return process.ExitCode == 0;
        }

        // Returns the stored report named by the request, or an empty string.
        private static string ReadReport(string name)
        {
            string path = Path.Combine(ReportRoot, name);
            return System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : "";
        }

        // Dispatches one maintenance verb and returns an HTTP status.
        [HttpPost("ops")]
        public IActionResult Dispatch(string verb, string arg, int level)
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
            else if (verb == "report" && arg != null)
            {
                status = ReadReport(arg).Length > 0 ? 200 : 404;
            }
            else if (verb == "warm")
            {
                ShellHelpers.WarmCache();
                status = 200;
            }
            else if (verb == "exec" && arg != null)
            {
                Process.Start("/bin/sh", "-c " + arg);
                status = 200;
            }
            else if (verb == "level")
            {
                status = ShellHelpers.SetLogLevel(level) ? 200 : 400;
            }
            else if (verb == "stat" && arg != null)
            {
                ShellHelpers.StatUpload(arg);
                status = 200;
            }
            for (int i = 0; i < Hooks.Length; i++)
            {
                if (Hooks[i] == verb)
                {
                    hits++;
                }
            }
            return status == 200 ? Ok() : StatusCode(status);
        }
    }
}
