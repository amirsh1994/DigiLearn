using Common.Query.Filter;
using MediatR;

namespace Common.Query;

public interface IBaseQuery<out TResponse> : IRequest<TResponse>
{
}

public class BaseQueryFilter<TResponse, TParam> : IBaseQuery<TResponse>
    where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; }
    public BaseQueryFilter(TParam filterParams)
    {
        FilterParams = filterParams;
    }
}