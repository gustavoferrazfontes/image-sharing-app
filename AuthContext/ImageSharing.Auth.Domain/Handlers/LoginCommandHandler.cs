using ImageSharing.Auth.Domain.Commands;
using ImageSharing.Auth.Domain.Interfaces;
using MediatR;

namespace ImageSharing.Auth.Domain.Handlers;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserEncryptService _userEncryptService;
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserEncryptService userEncryptService, IUserRepository userRepository)
    {
        _userEncryptService = userEncryptService;
        _userRepository = userRepository;
    }
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            throw new Exception("user or password invalid");


        var isMatch = _userEncryptService.IsMatch(user.Base64Salt, user.HashedPassword, request.Password);
        if (!isMatch)
            throw new Exception("user or password invalid");

        return new LoginResponse
        {
            Email = user.Email,
            Name = user.UserName
        };

    }
}
