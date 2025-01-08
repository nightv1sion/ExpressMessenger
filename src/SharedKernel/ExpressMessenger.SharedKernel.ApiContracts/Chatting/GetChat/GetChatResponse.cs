namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChat;

public sealed record GetChatResponse(GetChatResponse.ChatModel Chat)
{
    public sealed record ChatModel(
        Guid ChatId,
        IReadOnlyCollection<ChatModel.CompanionModel> Companions,
        string Type)
    {
        public sealed record CompanionModel(
            Guid UserId,
            uint DisplayNumber);
    };
}