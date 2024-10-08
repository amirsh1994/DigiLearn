﻿using Common.Query;
using CoreModule.Domain.Teachers.Enums;
using CoreModule.Query._DTOs;

namespace CoreModule.Query.Teacher._DTOs;

public class TeacherDto:BaseDto
{
    public string UserName { get; set; }

    public TeacherStatus Status { get;  set; }

    public string CvFileName { get;  set; }

    public CoreModuleUserDto User { get; set; }
}