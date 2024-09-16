using MediatR;

namespace Common.Query;

public interface IBaseQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IBaseQuery<TResponse>
{

}