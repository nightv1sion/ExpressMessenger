using ExpressMessenger.Common.Application;
using ExpressMessenger.UsersManagement.Domain.UserAggregate;

namespace ExpressMessenger.UsersManagement.Application.Authentication.RegisterUser;

internal sealed class RegisterUserHandler(
    IUserRepository userRepository,
    ITokenGenerator tokenGenerator,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, RegisteredUserModel>
{
    public async Task<RegisteredUserModel> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        uint biggestDisplayNumber = await userRepository.GetBiggestDisplayNumber(cancellationToken);
        
        User user = User.Create(biggestDisplayNumber + 1);
        TokenDto token = tokenGenerator.GenerateToken(user);

        await userRepository.InsertAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new RegisteredUserModel(
            user.Id,
            token.AccessToken,
            token.RefreshToken);
    }
}