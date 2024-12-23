using MediatR;

namespace ExpressMessenger.Common.Application;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;