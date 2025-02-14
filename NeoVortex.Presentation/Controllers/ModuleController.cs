using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Presentation.Common.Attributes;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class ModuleController : ApiController
{
    private readonly IModuleRepository _moduleRepository;

    public ModuleController(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    [HttpGet]
    [RequiredPermission("read:Roles")]
    public async Task<ActionResult> List()
    {
        var data = await _moduleRepository.ListAllAsync();
        return Ok(data);
    }
}