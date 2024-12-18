﻿using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using CoreModule.Domain.Courses.DomainServices;
using CoreModule.Domain.Courses.Enums;

namespace CoreModule.Domain.Courses.Models;

public class Course : AggregateRoot
{
    public Guid TeacherId { get; private set; }

    public Guid CategoryId { get; private set; }

    public Guid SubCategoryId { get; private set; }

    public string Slug { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string ImageName { get; private set; }

    public string? VideoName { get; private set; }

    public int Price { get; private set; }

    public DateTime LastUpdate { get; private set; }

    public CourseLevel CourseLevel { get; private set; }

    public CourseStatus CourseStatus { get; private set; }

    public CourseActionStatus Status { get;private set; }

    public SeoData SeoData { get; private set; }

    public List<Section> Sections { get; } = [];

    private Course()
    {

    }

    public Course(Guid teacherId, string title, string description, string imageName, string? videoName, int price, CourseLevel courseLevel, SeoData seoData, Guid subCategoryId, Guid categoryId, string slug, CourseActionStatus status, ICourseDomainService domainService)
    {
        Guard(title, description, imageName, slug);

        if (Slug != slug)
            if (domainService.IsSlugExists(slug))
                throw new InvalidDomainDataException("duplicated slug when create course");

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
        SubCategoryId = subCategoryId;
        CategoryId = categoryId;
        Slug = slug;
        Status = status;
    }

    public void Edit(string title, string description, string imageName, string? videoName, int price, CourseLevel courseLevel, CourseStatus courseStatus, SeoData seoData, Guid subCategoryId, Guid categoryId, string slug,CourseActionStatus courseAction, ICourseDomainService domainService)
    {
        Guard(title, description, imageName, slug);
        if (Slug != slug)
            if (domainService.IsSlugExists(slug))
                throw new InvalidDomainDataException("duplicated slug when Edit course");

        Title = title;
        Description = description;
        ImageName = imageName;
        VideoName = videoName;
        Price = price;
        LastUpdate = DateTime.Now;
        CourseLevel = courseLevel;
        CourseStatus = courseStatus;
        SeoData = seoData;
        SubCategoryId = subCategoryId;
        CategoryId = categoryId;
        Slug = slug;
        Status = courseAction;
    }

    private void Guard(string title, string description, string imageName, string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
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

    public void EditSection(Guid sectionId, string title, int displayOrder)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
        {
            throw new InvalidDomainDataException("section was not found");
        }
        section.Edit(title, displayOrder);
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

    public Episode AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoExtenstion, string? attachmentExtenstion, bool isActive, bool isFree,Guid sectionId, string englishTitle)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);

        if (section == null)
        {
            throw new InvalidDomainDataException("section not found");
        }

        var episodeCount = Sections.Sum(x => x.Episodes.Count());
        var episodeTitle = $"{episodeCount + 1}_{englishTitle}";

        string? atName = null;

        if (string.IsNullOrWhiteSpace(attachmentExtenstion) == false)
            atName = $"{episodeTitle}.{attachmentExtenstion}";

        var vidName = $"{episodeTitle}{videoExtenstion}"; //6_JavaSpring3.mp4

        if (isActive)
        {
            LastUpdate = DateTime.Now;
            if (CourseStatus == CourseStatus.StartSoon)
            {
                CourseStatus = CourseStatus.InProgress;
            }
        }

        return section.AddEpisode(title, token, timeSpan, vidName, atName, isActive,isFree, englishTitle);
    }

    public void AcceptEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(x => x.Episodes.Any(e => e.Id == episodeId && e.IsActive == false));

        if (section == null)
        {
            throw new InvalidDomainDataException("episode not found");
        }
        var episode = section.Episodes.First(x => x.Id == episodeId);
        episode.ToggleStatus();
        LastUpdate = DateTime.Now;
    }

    public Episode RemoveEpisode(Guid episodeId)
    {
        var section = Sections.FirstOrDefault(x => x.Episodes.Any(e => e.Id == episodeId));
        if (section == null)
        {
            throw new InvalidDomainDataException("episode not found for delete");
        }
        var episode = section.Episodes.First(x => x.Id == episodeId);
        section.Episodes.Remove(episode);
        return episode;
    }

    public void EditEpisode(Guid episodeId, Guid sectionId, string title, bool isActive,bool isFree ,TimeSpan timeSpan, string? attachmentName)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
            throw new InvalidDomainDataException("sectionIs is not valid....domain Edit Episode");

        var episode = section.Episodes.FirstOrDefault(x => x.Id == episodeId);
        if (episode == null)
            throw new InvalidDomainDataException("episodeId is not valid....domain Edit Episode");


        episode.Edit(title, isActive, isFree,timeSpan, attachmentName);
    }

    public Episode? GetEpisodeById(Guid sectionId, Guid episodeId)
    {
        var section = Sections.FirstOrDefault(x => x.Id == sectionId);
        if (section == null)
            return null;

        return section.Episodes.FirstOrDefault(x => x.Id == episodeId);
    }
}

public class Section : BaseEntity
{
    public Guid CourseId { get; private set; }

    public string Title { get; private set; }

    public List<Episode> Episodes { get; private set; } = [];

    public int DisplayOrder { get; private set; }


    public Section(string title, int displayOrder, Guid courseId)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        DisplayOrder = displayOrder;
        CourseId = courseId;
    }

    public Episode AddEpisode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive,bool isFree, string englishTitle)
    {
        var episode = new Episode(title, token, timeSpan, videoName, attachmentName, isActive, Id, englishTitle,isFree);
        Episodes.Add(episode);
        return episode;
    }

    public void Edit(string title, int displayOrder)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        DisplayOrder = displayOrder;
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

    public string? AttachmentName { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsFree { get; set; }


    public Episode(string title, Guid token, TimeSpan timeSpan, string videoName, string? attachmentName, bool isActive, Guid sectionId, string englishTitle,bool isFree)
    {
        Guard(videoName, englishTitle, title);
        Title = title;
        Token = token;
        TimeSpan = timeSpan;
        VideoName = videoName;
        AttachmentName = attachmentName;
        IsActive = isActive;
        SectionId = sectionId;
        EnglishTitle = englishTitle;
        IsFree=isFree;
    }

    void Guard(string videoName, string englishTitle, string title)
    {
        NullOrEmptyDomainDataException.CheckString(videoName, nameof(videoName));
        NullOrEmptyDomainDataException.CheckString(englishTitle, nameof(englishTitle));
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        if (englishTitle.IsUniCode())
        {
            throw new InvalidDomainDataException("invalid englishTitle");
        }
    }

    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }

    public void Edit(string title, bool isActive,bool isFree, TimeSpan timeSpan, string? attachmentName)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        IsActive = isActive;
        TimeSpan = timeSpan;
        IsFree=isFree;
        if (string.IsNullOrWhiteSpace(attachmentName) == false)
        {
            AttachmentName = attachmentName;
        }
    }


}
