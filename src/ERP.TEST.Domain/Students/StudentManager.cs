using ERP.TEST.Cources;
using ERP.TEST.Courses;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ERP.TEST.Students
{
    public class StudentManager: DomainService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<Course,Guid> _courseRepository;

        public StudentManager(IStudentRepository studentRepository, IRepository<Course, Guid> courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<Student> CreateAsync(string name,string address,int age,string phone, [CanBeNull] Guid[] courseIds)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var existingStudent = await _studentRepository.FindByIdAsync(name,Guid.Empty);
            if (existingStudent)
            {
                throw new StudentAlreadyExistsException(name);
            }
            var student = new Student(GuidGenerator.Create(),name,address,phone,age);
            await SetCourseAsync(student, courseIds);
            return student;

        }

        public async Task<Student> EditAsync( Student student, string name, string address, int age, string phone, [CanBeNull] Guid[] courseIds)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            var existingStudent = await _studentRepository.FindByIdAsync(name,student.Id);
            if (existingStudent)
            {
                throw new StudentAlreadyExistsException(name);
            }
            student.SetName(name);
            student.Address = address;
            student.Phone = phone;
            student.Age=age;
            await SetCourseAsync(student, courseIds);
            return student;
        }

        private async Task SetCourseAsync(Student student, [CanBeNull] Guid[] courIds)
        {
            
            if (!courIds.Any())
            {
                return;
            }

            foreach (var courseId in courIds)
            {
                var existingCourse = await _courseRepository.AnyAsync(x=> x.Id == courseId);
                if (!existingCourse)
                {
                    throw new UserFriendlyException("This course GUID is invalid: "+ courseId);
                }
                student.AddCourse(courseId);
            
            }
        }
    }

}

