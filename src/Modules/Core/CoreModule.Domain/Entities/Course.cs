using Common.Domain;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Enums;

namespace CoreModule.Domain.Entities;

public class Course : BaseEntity
{
    public Guid TeacherId { get;private set; }

    public string Title { get;private set; }

    public string Description { get; private set; }

    public string ImageName { get; private set; }

    public string VideoName { get; private set; }

    public int Price { get; private set; }

    public DateTime LastUpdate { get; private set; }

    public CourseLevel CourseLevel { get; private set; }

    public CourseStatus CourseStatus { get; private set; }

    public SeoData SeoData { get; private set; }

    public IEnumerable<Section> Sections { get; }

    public Course(Guid teacherId, string title, string description, string imageName, string videoName, int price, DateTime lastUpdate, CourseLevel courseLevel, SeoData seoData)
    {
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = lastUpdate;
        CourseLevel = courseLevel;
        CourseStatus = CourseStatus.StartSoon;
        SeoData = seoData;
    }


}

public class Section:BaseEntity
{
    public string Title { get; private set; } 

    public IEnumerable<Episode> Episodes { get; }

    public int DisplayOrder { get; private set; }

    public Section(string title, int displayOrder)
    {
        Title = title;
        DisplayOrder = displayOrder;
    }
}

public class Episode:BaseEntity
{
    public string Title { get; private set; } 

    public Guid Token { get; private set; } 

    public TimeSpan TimeSpan { get; private set; } 

    public string VideoName { get; private set; } 

    public string AttachmentName { get; private set; }

    public bool IsActive { get; private set; }

    public Episode(string title, Guid token, TimeSpan timeSpan, string videoName, string attachmentName, bool isActive)
    {
        Title = title;
        Token = token;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
    }
}
