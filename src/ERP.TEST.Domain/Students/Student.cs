using ERP.TEST.Cources;
using ERP.TEST.Courses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Guids;

namespace ERP.TEST.Students
{
    public class Student:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public virtual ICollection<StudentCourse> Courses { get;  set; }
        public virtual List<Course> RelationCourses { get; set; } = new(); //for navigation

        public Student() { }
        //------------------------------------------------------------------------
        public Student(Guid id, string name, string address, string phone, int age)
           : base(id)
        {
            SetName(name);
            this.Address = address;
            Phone = phone;
            this.Age = age;
            this.Courses = new Collection<StudentCourse>();
        }

        internal void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), StudentConsts.MaxNameLength);
        }

        internal void AddCourse(Guid courseId)
        {
            Check.NotNull(courseId, nameof(courseId));

            if (IsInCourse(courseId))
            {
                return;
            }

            Courses.Add(new StudentCourse(Guid.NewGuid(),studentId:Id, courseId:courseId));
        }

        internal void RemoveCourse(Guid courseId)
        {
            Check.NotNull(courseId, nameof(courseId));

            if (!IsInCourse(courseId))
            {
                return;
            }

             Courses.RemoveAll(x => x.CourseId == courseId);

        }

        internal void RemoveAllCourseExceptGivenIds(List<Guid> courseIds)
        {
            Check.NotNullOrEmpty(courseIds, nameof(courseIds));

            Courses.RemoveAll(x => !courseIds.Contains(x.CourseId));
        }

        internal void RemoveAllCourse( Guid stdId)
        {
            Courses.RemoveAll(x => x.StudentId == stdId);
        }

        internal bool IsInCourse(Guid courseId)
        {
            return Courses.Any(x => x.CourseId == courseId);
        }
    }
}

