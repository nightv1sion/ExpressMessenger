namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.SendPersonalMessage;

public sealed record SendPersonalMessageRequest(
    string Text,
    Guid RecipientId);