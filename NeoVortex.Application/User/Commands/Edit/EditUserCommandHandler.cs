using ErrorOr;
using MediatR;
using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Application.Interfaces.Services;
using NeoVortex.Domain.Errors;

namespace NeoVortex.Application.User.Commands.Edit;

public class EditUserCommandHandler : IRequestHandler<EditUserCommand, ErrorOr<Updated>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    
    public EditUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }
    
    public async Task<ErrorOr<Updated>> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null) return Errors.User.NotFound;
        
        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = _passwordService.HashPassword(request.Password);
        user.RoleId = request.RoleId;
        
        await _userRepository.UpdateAsync(user);

        return Result.Updated;
    }
}