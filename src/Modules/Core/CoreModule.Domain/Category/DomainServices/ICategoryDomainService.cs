namespace CoreModule.Domain.Category.DomainServices;

public interface ICategoryDomainService
{
    bool IsExitsSlug(string slug);
}