using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ERP.TEST.Courses
{
    public class CreateUpdateCourseDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
