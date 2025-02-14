using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeoVortex.Application.Document.Queries.List;
using NeoVortex.Application.Interfaces.Auth;
using NeoVortex.Domain.Errors;
using NeoVortex.Presentation.Common.Attributes;
using NeoVortex.Presentation.Common.Controllers;
using NeoVortex.Presentation.Utilities;

namespace NeoVortex.Presentation.Controllers;

public class DocumentController : ApiController
{
    private readonly IAuthUtilities _authUtilities;
    private readonly IMediator _mediator;

    public DocumentController(IAuthUtilities authUtilities, IMediator mediator)
    {
        _authUtilities = authUtilities;
        _mediator = mediator;
    }

    public record ListDocumentRequest(int Page, int PageSize, Guid? UserId);
    
    public record CreateDocumentRequest(Guid UserId, string Title);
    
    
    [HttpGet]
    [RequiredPermission("read:Documento")]
    public async Task<IActionResult> List([FromQuery] ListDocumentRequest request)
    {
        if (!_authUtilities.HasSuperAccess() && request.UserId != _authUtilities.GetUserId())
            return Problem(Errors.User.Unauthorized);

        var query = new ListDocumentsQuery(request.Page, request.PageSize, request.UserId);
        
        var result = await _mediator.Send(query);

        return result.Match(Ok, Problem);
    }
    
    [HttpPost]
    [RequiredPermission("create:Documento")]
    public async Task<IActionResult> Create([FromBody] CreateDocumentRequest request)
    {
        return Ok();
    }
    
    [HttpPut]
    [RequiredPermission("update:Documento")]
    public async Task<IActionResult> Update([FromBody] CreateDocumentRequest request)
    {
        return Ok();
    }
}