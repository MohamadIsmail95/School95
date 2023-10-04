using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace ERP.TEST.Students
{
    public class StudentDto:EntityDto<Guid>
    {
        public string Name { get;set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string[] CourseNames { get; set; }

    }
}
