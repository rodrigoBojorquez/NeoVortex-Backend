using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Roles.Common;

namespace NeoVortex.Application.Roles.Queries.List;

public record ListRolesQuery(int Page, int PageSize, string? Name) : IRequest<ErrorOr<ListResult<RoleResult>>>;