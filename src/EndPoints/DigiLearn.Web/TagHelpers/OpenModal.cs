﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DigiLearn.Web.TagHelpers;

public class OpenModal:TagHelper
{
    public string Url { get; set; }

    public string ModalTitle { get; set; } = "";

    public string Class { get; set; } = "btn btn-success btn-sm mb-2";

    public bool IsBootstrap4 { get; set; } = false;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("class",Class);
        output.Attributes.Add("onClick", $"OpenModal('{Url}','defaultModal','{ModalTitle}','lg','undefined',{IsBootstrap4.ToString().ToLower()})");

        base.Process(context, output);
    }
}