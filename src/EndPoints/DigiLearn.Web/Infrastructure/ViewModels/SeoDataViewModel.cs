using System.ComponentModel.DataAnnotations;
using Common.Application;
using Common.Domain.ValueObjects;

namespace DigiLearn.Web.Infrastructure.ViewModels;

public class SeoDataViewModel
{
    public string? MetaTitle { get; set; }

    [DataType(DataType.MultilineText)]
    public string? MetaDescription { get; set; }

    public string? MetaKeyWords { get; set; }

    [DataType(DataType.Url)]
    public string? Canonical { get; set; }

    public SeoData Map()
    {
        return new SeoData(MetaKeyWords, MetaDescription, MetaTitle, Canonical);
    }


    public static SeoDataViewModel ToViewModel(SeoData seoData)
    {
        return new SeoDataViewModel
        {
            MetaTitle = seoData.MetaTitle,
            MetaDescription = seoData.MetaDescription,
            MetaKeyWords = seoData.MetaKeyWords,
            Canonical = seoData.Canonical
        };
    }
}