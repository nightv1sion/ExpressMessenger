namespace ExpressMessenger.UsersManagement.Application.Authentication;

public sealed record TokenDto(
    string AccessToken,
    string RefreshToken);