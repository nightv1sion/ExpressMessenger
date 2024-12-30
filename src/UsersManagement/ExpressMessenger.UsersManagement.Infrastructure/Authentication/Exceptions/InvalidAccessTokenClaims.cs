namespace ExpressMessenger.UsersManagement.Infrastructure.Authentication.Exceptions;

public sealed class InvalidAccessTokenClaims() : Exception("Access token has invalid claims");