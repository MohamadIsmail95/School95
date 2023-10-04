using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ERP.TEST.Courses
{
    public class StudentCourse: Entity
    {
        public Guid StudentId { get;  set; }
        public Guid CourseId { get;  set; }
        public StudentCourse() { }
        public StudentCourse(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public override object[] GetKeys()
        {
            return new object[] { StudentId, CourseId };
        }
    }
}
