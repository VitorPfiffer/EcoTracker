using EcoTracker.Core.Swagger.CodeInjection;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.Core.Swagger.Controllers
{
    [Route("")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class SwaggerController : ControllerBase
    {
        private readonly CssManager _cssManager;
        private readonly JavascriptManager _javascriptManager;

        public SwaggerController(CssManager cssManager, JavascriptManager javascriptManager)
        {
            _cssManager = cssManager;
            _javascriptManager = javascriptManager;
        }

        [HttpGet("/swagger/custom.css")]
        public async Task<IActionResult> Get() => Content(_cssManager.GetCSS(), "text/css");

        [HttpGet("/swagger/custom.js")]
        public async Task<IActionResult> GetJS() => Content(_javascriptManager.GetJavascript(), "text/javascript");
    }
}
