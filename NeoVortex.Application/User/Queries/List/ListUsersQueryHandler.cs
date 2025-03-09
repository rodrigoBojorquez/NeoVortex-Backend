using ErrorOr;
using MediatR;
using NeoVortex.Application.Common.Results;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.User.Common;

namespace NeoVortex.Application.User.Queries.List;

public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, ErrorOr<ListResult<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ListResult<UserResult>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var (page, pageSize, search) = request;

        var data = await _userRepository.ListWithRoleAsync(page, pageSize, u => search == null || u.Name.Contains(search));

        return new ListResult<UserResult>(data.Page, data.PageSize, data.TotalItems,
            data.Items.Select(u => new UserResult(u.Id, u.Name, u.Email, u.Role.Name, u.RoleId)).ToList());
    }
}