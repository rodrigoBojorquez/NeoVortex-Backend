using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Roles.Common;

namespace NeoVortex.Application.Roles.Queries.List;

public class ListRolesQueryHandler : IRequestHandler<ListRolesQuery, ErrorOr<ListResult<RoleResult>>>
{
    private readonly IRoleRepository _roleRepository;

    public ListRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<ListResult<RoleResult>>> Handle(ListRolesQuery request,
        CancellationToken cancellationToken)
    {
        var (page, pageSize, title) = request;

        var data = await _roleRepository.ListAsync(page, pageSize, r => title == null || r.Name.Contains(title));
        
        return new ListResult<RoleResult>(
            data.Page, 
            data.PageSize, 
            data.TotalItems,
            data.Items.Select(r => new RoleResult(
                r.Id, 
                r.Name, 
                r.Description, 
                r.Permissions.Select(p => p.Id).ToList()
            )).ToList()
        );
    }
}