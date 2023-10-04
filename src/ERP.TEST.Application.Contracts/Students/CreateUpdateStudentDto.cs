using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.TEST.Students
{
    public class CreateUpdateStudentDto
    {
        public  Guid ? Id {get;set;}
        public string Name { get;  set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string[] CourseNames { get; set; }

    }
}
