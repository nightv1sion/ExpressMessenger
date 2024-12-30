using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Authentication.RefreshToken;

internal sealed class RefreshTokenHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenGenerator tokenGenerator) : ICommandHandler<RefreshTokenCommand, RefreshTokenModel>
{
    public async Task<RefreshTokenModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.TryGetById(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("User not found");
        }
        
        TokenDto token = tokenGenerator.RefreshToken(user, request.AccessToken, request.RefreshToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new RefreshTokenModel(token.AccessToken, token.RefreshToken);
    }
}