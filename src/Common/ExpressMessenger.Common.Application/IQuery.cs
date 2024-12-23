using MediatR;

namespace ExpressMessenger.Common.Application;

public interface IQuery<out TResponse> : IRequest<TResponse>;