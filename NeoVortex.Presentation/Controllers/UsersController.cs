using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.User.Commands.Add;
using NeoVortex.Application.User.Commands.Edit;
using NeoVortex.Application.User.Common;
using NeoVortex.Application.User.Queries.List;
using NeoVortex.Domain.Errors;
using NeoVortex.Presentation.Common.Controllers;

namespace NeoVortex.Presentation.Controllers;

public class UsersController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public UsersController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public record ListUsersRequest(int Page = 1, string? Search = null, int PageSize = 10);
    public record AddUserRequest(string Name, string Email, string Password, Guid RoleId);
    public record UpdateUserRequest(Guid Id, string Name, string Email, string Password, Guid RoleId);

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ListUsersRequest request)
    {
        var query = new ListUsersQuery(request.Page, request.PageSize, request.Search);
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _userRepository.IncludeRoleAsync(id);

        if (user is null) return Problem(Errors.User.NotFound);

        return Ok(new UserResult(user.Id, user.Name, user.Email, user.Role.Name, user.RoleId));
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(AddUserRequest request)
    {
        var command = new AddUserCommand(request.Name, request.Email, request.Password, request.RoleId);
        var result = await _mediator.Send(command);
        
        return result.Match(created => Ok(created), Problem);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user is not null)
            await _userRepository.DeleteAsync(user.Id);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserRequest request)
    {
        var command = new EditUserCommand(request.Id, request.Name, request.Email, request.Password, request.RoleId);
        var result = await _mediator.Send(command);
        
        return result.Match(updated => Ok(updated), Problem);
    }
}