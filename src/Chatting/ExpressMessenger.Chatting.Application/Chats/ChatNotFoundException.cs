namespace ExpressMessenger.Chatting.Application.Chats;

public class ChatNotFoundException(Guid UserId, Guid ChatId) : Exception($"Chat {ChatId} not found for user {UserId}");