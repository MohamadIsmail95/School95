using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ERP.TEST.Courses
{
    public class CourseDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
