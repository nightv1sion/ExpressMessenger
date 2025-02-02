namespace ExpressMessenger.SharedKernel.ApiContracts.Chatting.GetChat;

public sealed record GetChatResponse(GetChatResponse.ChatModel Chat)
{
    public sealed record ChatModel(
        Guid ChatId,
        string Type,
        IReadOnlyCollection<ChatModel.CompanionModel> Companions,
        IReadOnlyCollection<ChatModel.MessageModel> Messages)
    {
        public sealed record CompanionModel(
            Guid UserId,
            string UserNames);

        public sealed record MessageModel(
            Guid Id,
            string SenderUserName,
            string Text,
            bool IsMine,
            DateTimeOffset Sent);
    };
}