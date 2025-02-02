namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.CreateChat;

public sealed record CreateChatRequest(IReadOnlyCollection<Guid> CompanionIds);