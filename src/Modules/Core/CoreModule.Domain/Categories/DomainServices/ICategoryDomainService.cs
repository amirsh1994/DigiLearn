namespace CoreModule.Domain.Categories.DomainServices;

public interface ICategoryDomainService
{
    bool IsExitsSlug(string slug);
}