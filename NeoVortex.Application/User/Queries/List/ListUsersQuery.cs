using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.User.Common;

namespace NeoVortex.Application.User.Queries.List;

public record ListUsersQuery(int Page, int PageSize, string? Name) : IRequest<ErrorOr<ListResult<UserResult>>>;