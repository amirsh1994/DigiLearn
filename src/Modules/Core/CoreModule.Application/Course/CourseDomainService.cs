using Common.Domain.Utils;
using CoreModule.Domain.Courses.DomainServices;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Application.Course;

public class CourseDomainService(ICourseRepository repository):ICourseDomainService
{
    public bool IsSlugExists(string slug)
    {
        return repository.Exists(x => x.Slug == slug.ToSlug());
    }
}