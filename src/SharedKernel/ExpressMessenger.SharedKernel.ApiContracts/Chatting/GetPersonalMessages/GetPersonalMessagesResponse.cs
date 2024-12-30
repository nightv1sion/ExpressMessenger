namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetPersonalMessages;

public sealed record GetPersonalMessagesResponse(
    IReadOnlyCollection<GetPersonalMessagesResponse.MessageModel> Messages)
{
    public sealed record MessageModel(
        Guid Id,
        string Text,
        DateTimeOffset Sent);
}