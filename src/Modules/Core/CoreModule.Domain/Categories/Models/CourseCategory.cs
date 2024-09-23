using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using CoreModule.Domain.Categories.DomainServices;

namespace CoreModule.Domain.Categories.Models;

public class CourseCategory:AggregateRoot
{
    public string Title { get;private set; }

    public string Slug { get;private set; }

    public Guid? ParentId { get; private set; }

    public CourseCategory(string title, string slug, Guid? parentId,ICategoryDomainService categoryDomainService)
    {
        Guard(title, slug);
        if (categoryDomainService.IsExitsSlug(slug))
        {
            throw new InvalidDomainDataException("duplicated data slug is already exists");
        }
        Title = title;
        Slug = slug;
        ParentId = parentId;
    }

    public void Edit(string title, string slug,ICategoryDomainService categoryDomainService)
    {
        Guard(title,slug);
        if (slug!=Slug)
        {
            if (categoryDomainService.IsExitsSlug(slug))
            {
                throw new InvalidDomainDataException("duplicated slug for edit is already exists");
            }
        }
        Title=title;
        Slug=slug;
    }

    void Guard(string title,string slug)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug,nameof(slug));
        if (slug.IsUniCode())
        {
            throw new InvalidDomainDataException("slug must be english");
        }
    }
}