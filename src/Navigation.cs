// Outbound navigation for the smoke service. Deliberately vulnerable smoke target; see README.md.
using Microsoft.AspNetCore.Mvc;

namespace Smoke
{
    [ApiController]
    [Route("nav")]
    public class NavigationController : ControllerBase
    {
        // Sends the client on to the link it asked for.
        [HttpGet("follow")]
        public IActionResult Follow(string target)
        {
            return Redirect(target);
        }
    }
}
