using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TEST.Courses
{
    public interface ICourseAppService: ICrudAppService<CourseDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCourseDto, CreateUpdateCourseDto>
    {
    }
}
