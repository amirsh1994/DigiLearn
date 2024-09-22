using System.Runtime.Versioning;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Enums;

namespace CoreModule.Domain.Entities;

public class Course : BaseEntity
{
    public Guid TeacherId { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string ImageName { get; private set; }

    public string? VideoName { get; private set; }

    public int Price { get; private set; }

    public DateTime LastUpdate { get; private set; }

    public CourseLevel CourseLevel { get; private set; }

    public CourseStatus CourseStatus { get; private set; }

    public SeoData SeoData { get; private set; }

    public List<Section> Sections { get; } = [];

    public Course(Guid teacherId, string title, string description, string imageName, string? videoName, int price, CourseLevel courseLevel, SeoData seoData)
    {
        Guard(title, description, imageName);
        TeacherId = teacherId;
        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        CourseLevel = courseLevel;
        CourseStatus = CourseStatus.StartSoon;
        SeoData = seoData;
    }

    private void Guard(string title, string description, string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
    }

    public void AddSection(string title, int displayOrder)
    {
        if (Sections.Any(x => x.Title == title))
        {
            throw new InvalidDomainDataException("duplicate title  from domain");
        }

        Sections.Add(new Section(title, displayOrder, Id));
    }

    public void RemoveSection(Guid sectionId)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
        {
            throw new InvalidDomainDataException("section not found");
        }
        Sections.Remove(section);
    }

    public void AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoExtenstion, string?  attachmentExtenstion, bool isActive,Guid sectionId, string englishTitle)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);

        if (section == null)
        {
            throw new InvalidDomainDataException("section not found");
        }

        var episodeCount = Sections.Sum(x => x.Episodes.Count());
        var episodeTitle = $"{episodeCount+1}_{englishTitle}";

        string? atName = null;

        if (string.IsNullOrWhiteSpace(attachmentExtenstion) == false)
            atName = $"{episodeTitle}.{attachmentExtenstion}";

        var vidName = $"{episodeTitle}.{videoExtenstion}";
        section.AddEpisode(title,token,timeSpan,vidName,atName, isActive, englishTitle);
    }
}

public class Section:BaseEntity
{
    public Guid CourseId { get; private set; }

    public string Title { get; private set; }

    public IEnumerable<Episode> Episodes { get; private set; } = [];

    public int DisplayOrder { get; private set; }

    public Section(string title, int displayOrder, Guid courseId)
    {
        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;
    }

    public void AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoName, string?  attachmentName, bool isActive, string englishTitle)
    {
        Episodes = Episodes.Append(new Episode(title, token, timeSpan, videoName, attachmentName, isActive, Id,englishTitle));
    }
}

public class Episode : BaseEntity
{
    public Guid SectionId { get; private set; }

    public string Title { get; private set; }

    public string EnglishTitle { get; private set; }

    public Guid Token { get; private set; }

    public TimeSpan TimeSpan { get; private set; }

    public string VideoName { get; private set; }

    public string?  AttachmentName { get; private set; }

    public bool IsActive { get; private set; }

    public Episode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, Guid sectionId, string englishTitle)
    {
        Guard(videoName,englishTitle,title);
        Title = title;
        Token = token;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        SectionId = sectionId;
        EnglishTitle = englishTitle;
    }

    void Guard(string videoName,string englishTitle,string title)
    {
        NullOrEmptyDomainDataException.CheckString(videoName,nameof(videoName));
        NullOrEmptyDomainDataException.CheckString(englishTitle,nameof(englishTitle));
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        if (englishTitle.IsUniCode())
        {
            throw new InvalidDomainDataException("invalid englishTitle");
        }
    }
}
