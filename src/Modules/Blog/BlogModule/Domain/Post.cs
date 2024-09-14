using Common.Domain;

namespace BlogModule.Domain;

class Post : BaseEntity
{
    public string Title { get; set; }

    public string Slug { get; set; }

    public Guid UserId { get; set; }

    public string OwnerName { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public long Visit { get; set; }

    public string ImageName { get; set; }



}