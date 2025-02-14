using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Roles.Commands.Create;
using NeoVortex.Application.Roles.Commands.Update;
using NeoVortex.Application.Roles.Queries.List;
using NeoVortex.Presentation.Common.Attributes;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class RoleController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IRoleRepository _roleRepository;

    public RoleController(IMediator mediator, IRoleRepository roleRepository)
    {
        _mediator = mediator;
        _roleRepository = roleRepository;
    }

    public record ListRolesRequest(int Page, int PageSize, string? Search);
    public record CreateRoleRequest(string Name, List<Guid> Permissions, string? Description);
    public record UpdateRoleRequest(Guid Id, string Name, List<Guid> Permissions, string? Description);


    [HttpGet]
    [RequiredPermission("read:Roles")]
    public async Task<IActionResult> List([FromQuery] ListRolesRequest request)
    {
        var query = new ListRolesQuery(request.Page, request.PageSize, request.Search);
        var result = await _mediator.Send(query);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
    
    [HttpPost]
    [RequiredPermission("create:Roles")]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        var command = new CreateRoleCommand(request.Name, request.Permissions,request.Description);
        var result = await _mediator.Send(command);
        
        return result.Match(created => Ok(created), Problem);
    }
    
    [HttpPut]
    [RequiredPermission("update:Roles")]
    public async Task<IActionResult> Update(UpdateRoleRequest request)
    {
        var command = new UpdateRoleCommand(request.Id, request.Name, request.Permissions, request.Description);
        var result = await _mediator.Send(command);
        
        return result.Match(updated => Ok(updated), Problem);
    }
    
    [HttpDelete]
    [RequiredPermission("delete:Roles")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _roleRepository.DeleteAsync(id);
        return Ok(Result.Deleted);
    }
}