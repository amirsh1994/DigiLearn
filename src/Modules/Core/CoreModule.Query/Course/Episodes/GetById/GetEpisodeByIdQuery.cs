using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Course._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Course.Episodes.GetById;

public record GetEpisodeByIdQuery(Guid EpisodeId) : IBaseQuery<EpisodeDto?>;



internal  class GetEpisodeByIdQueryHandler(QueryContext db):IBaseQueryHandler<GetEpisodeByIdQuery, EpisodeDto?>
{
    public async Task<EpisodeDto?> Handle(GetEpisodeByIdQuery request, CancellationToken cancellationToken)
    {
        var episode = await db.Episodes.FirstOrDefaultAsync(x => x.Id == request.EpisodeId, cancellationToken);
        if (episode==null)
        {
            return null;
        }

        return new EpisodeDto
        {
            Id = episode.Id,
            CreationDate = episode.CreationDate,
            SectionId = episode.SectionId,
            Title = episode.Title,
            EnglishTitle = episode.EnglishTitle,
            Token = episode.Token,
            TimeSpan = episode.TimeSpan,
            VideoName = episode.VideoName,
            AttachmentName = episode.AttachmentName,
            IsActive = episode.IsActive,
        };

    }
}