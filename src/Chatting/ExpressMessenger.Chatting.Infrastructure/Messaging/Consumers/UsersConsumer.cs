using ExpressMessenger.Chatting.Application.Users.ActualizeDeleted;
using ExpressMessenger.SharedKernel.ApiContracts.Messaging.UserDeleted;
using MassTransit;
using MediatR;

namespace ExpressMessenger.Chatting.Infrastructure.Messaging.Consumers;

public sealed class UsersConsumer(ISender sender) : IConsumer<UserDeleted>
{
    public Task Consume(ConsumeContext<UserDeleted> context)
    {
        ActualizeDeletedUserCommand command = new(context.Message.UserId);
        return sender.Send(command, context.CancellationToken);
    }
}

public sealed class UsersConsumerDefinition : ConsumerDefinition<UsersConsumer>
{
    public UsersConsumerDefinition()
    {
        EndpointName = "users-topic";
    }
}