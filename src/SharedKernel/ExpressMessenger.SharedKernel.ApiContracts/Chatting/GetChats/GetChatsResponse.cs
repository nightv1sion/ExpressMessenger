namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChats;

public sealed record GetChatsResponse(
    IReadOnlyCollection<GetChatsResponse.ChatModel> Chats)
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