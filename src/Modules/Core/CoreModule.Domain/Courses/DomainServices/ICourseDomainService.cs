namespace CoreModule.Domain.Courses.DomainServices;

public interface ICourseDomainService
{
    bool IsSlugExists(string slug);
}