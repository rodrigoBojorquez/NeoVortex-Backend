using Microsoft.AspNetCore.Mvc;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class UserController : ApiController
{
    [HttpGet]
    public IActionResult List()
    {
        return Ok();
    }
}