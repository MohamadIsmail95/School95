using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ERP.TEST.Students
{
    public class CreateUpdateStudentDto
    {
        public  Guid ? Id {get;set;}
        [Required]
        public string Name { get;  set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public Guid[] CoursesId { get; set; }

    }
}
