namespace ExpressMessenger.Chatting.Application.Chats;

public class ChatNotFoundException(Guid userId, Guid chatId) : Exception($"Chat {chatId} not found for user {userId}");