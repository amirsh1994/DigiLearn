﻿using Common.Domain;

namespace BlogModule.Domain;

 class Category:BaseEntity
{
    public string Title { get; set; }

    public string slug { get; set; }
}