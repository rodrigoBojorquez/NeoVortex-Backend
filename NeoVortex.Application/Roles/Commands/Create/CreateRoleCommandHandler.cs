using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;

namespace NeoVortex.Application.Roles.Commands.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ErrorOr<Created>>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<Created>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = request.Name,
            Description = request.Description
        };

        await _roleRepository.InsertAsync(role);
        await _roleRepository.AssignPermissionsAsync(role.Id, request.Permissions);

        return Result.Created;
    }
}