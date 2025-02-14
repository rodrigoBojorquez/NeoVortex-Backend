using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class PermissionController : ApiController
{
    private readonly IPermissionRepository _permissionRepository;

    public PermissionController(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var data = await _permissionRepository.ListAllAsync();
        
        return Ok(data);
    }
}