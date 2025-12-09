// Feed import for the smoke service. Deliberately vulnerable smoke target; see README.md.
using System.IO;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace Smoke
{
    [ApiController]
    [Route("feeds")]
    public class FeedsController : ControllerBase
    {
        // Imports a feed document posted by the client and returns its root element name.
        [HttpPost("import")]
        public IActionResult ImportFeed(string body)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.XmlResolver = new XmlUrlResolver();
            XmlDocument document = new XmlDocument();
            document.Load(XmlReader.Create(new StringReader(body), settings));
            return Ok(document.DocumentElement.Name);
        }
    }
}
