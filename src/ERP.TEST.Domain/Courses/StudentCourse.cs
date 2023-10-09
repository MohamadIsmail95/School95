using AutoMapper.Configuration.Annotations;
using ERP.TEST.Cources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace ERP.TEST.Courses
{
    public class StudentCourse: Entity<Guid>
    {
        
        public Guid StudentId { get;  set; }
        public Guid CourseId { get;  set; }
        public StudentCourse() { }
        public StudentCourse(Guid id,Guid studentId, Guid courseId): base(id)
        { 
            StudentId = studentId;
            CourseId = courseId;
        }

    }
}
