using Common.Domain.Utils;
using CoreModule.Domain.Categories.DomainServices;
using CoreModule.Domain.Categories.Repository;

namespace CoreModule.Application.Category;

public class CourseCategoryDomainService(ICourseCategoryRepository repository):ICategoryDomainService
{
    public bool IsExitsSlug(string slug)
    {
        return repository.Exists(x => x.Slug == slug.ToSlug());
    }
}