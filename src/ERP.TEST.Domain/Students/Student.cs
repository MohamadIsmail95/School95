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

namespace ERP.TEST.Students
{
    public class Student:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public virtual ICollection<StudentCourse> Courses { get; private set; }
        public Student() { }
        //------------------------------------------------------------------------
        public Student(Guid id, string name, string address, string phone, int age)
           : base(id)
        {
            this.Id = id;
            SetName(name);
            this.Address = address;
            Phone = phone;
            this.Age = age;
            this.Courses = new Collection<StudentCourse>();
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), StudentConsts.MaxNameLength);
        }

        public void AddCourse(Guid courseId)
        {
            Check.NotNull(courseId, nameof(courseId));

            if (IsInCourse(courseId))
            {
                return;
            }

            Courses.Add(new StudentCourse(studentId:Id, courseId:courseId));
        }

        public void RemoveCourse(Guid courseId)
        {
            Check.NotNull(courseId, nameof(courseId));

            if (!IsInCourse(courseId))
            {
                return;
            }

            Courses.RemoveAll(x => x.CourseId == courseId);
        }

        public void RemoveAllCourseExceptGivenIds(List<Guid> courseIds)
        {
            Check.NotNullOrEmpty(courseIds, nameof(courseIds));

            Courses.RemoveAll(x => !courseIds.Contains(x.CourseId));
        }

        public void RemoveAllCourse()
        {
            Courses.RemoveAll(x => x.StudentId == Id);
        }

        private bool IsInCourse(Guid courseId)
        {
            return Courses.Any(x => x.CourseId == courseId);
        }
    }
}

