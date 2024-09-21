using Common.Query;
using Microsoft.EntityFrameworkCore;
using UserModule.Core.Queries._DTOs;
using UserModule.Data;

namespace UserModule.Core.Queries.Notifications.GetFilter;

public class GetNotificationsByFilterQuery(NotificationFilterParams filterParams) : BaseQueryFilter<NotificationFilterResult, NotificationFilterParams>(filterParams);


public class GetNotificationsByFilterQueryHandler(UserContext db) : IBaseQueryHandler<GetNotificationsByFilterQuery, NotificationFilterResult>
{
    public async Task<NotificationFilterResult> Handle(GetNotificationsByFilterQuery request, CancellationToken cancellationToken)
    {
        var @param = request.FilterParams;
        var result = db.Notifications.Where(x => x.UserId == @param.UserId).AsQueryable();
        if (@param.IsSeen != null)
        {
            if (@param.IsSeen.Value == true)
            {
                result = result.Where(x => x.IsSeen);
            }
            else
            {
                result = result.Where(x => x.IsSeen == false);
            }
        }

        var skip = (@param.PageId - 1) * @param.Take;

        var data = new NotificationFilterResult
        {
            Data = await result.Skip(skip).Take(@param.Take).Select(x => new NotificationFilterData
            {
                Id = x.Id,
                CreationDate = x.CreationDate,
                UserId = x.UserId,
                Text = x.Text,
                Title = x.Title,
                IsSeen = x.IsSeen
            }).ToListAsync(cancellationToken)
        };
        data.GeneratePaging(result, @param.Take, @param.PageId);
        return data;
    }
}