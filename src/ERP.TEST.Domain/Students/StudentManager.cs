using ERP.TEST.Cources;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ERP.TEST.Students
{
    public class StudentManager: DomainService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<Course, Guid> _courceRepository;

        public StudentManager(IStudentRepository studentRepository, IRepository<Course, Guid> courceRepository)
        {
            _studentRepository = studentRepository;
            _courceRepository = courceRepository;
        }

        public async Task<object> CreateAsync(string name,string address,int age,string phone, [CanBeNull] string[] courseNames)
        {
            var student = new Student(GuidGenerator.Create(),name,address,phone,age);
            await SetCourseAsync(student, courseNames);
            await _studentRepository.InsertAsync(student);
            return student;

        }

        public async Task<object> UpdateAsync( Student student, string name, string address, int age, string phone, [CanBeNull] string[] courseNames)
        {
            student.Address= address;
            student.Age= age;
            student.Phone= phone;
            student.SetName(name);
            await SetCourseAsync(student, courseNames);

           return  await _studentRepository.UpdateAsync(student);
        }

        private async Task SetCourseAsync(Student student, [CanBeNull] string[] courseNames)
        {
            if (courseNames == null || !courseNames.Any())
            {
                student.RemoveAllCourse();
                return;
            }

            var query = (await _courceRepository.GetQueryableAsync())
                .Where(x => courseNames.Contains(x.Name))
                .Select(x => x.Id)
                .Distinct();

            var courseIds = await AsyncExecuter.ToListAsync(query);
            if (!courseIds.Any())
            {
                return;
            }

             student.RemoveAllCourseExceptGivenIds(courseIds);

            foreach (var courseId in courseIds)
            {
                student.AddCourse(courseId);
            }
        }
    }

}

