namespace ExpressMessenger.BlazorWebApp.Apis;

public sealed class ApisSettings
{
    public const string SectionName = "Apis";

    public Api UsersManagement { get; init; } = null!;

    public sealed class Api
    {
        public string Url { get; init; } = null!;
    }
}