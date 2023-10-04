using ERP.TEST.Cources;
using ERP.TEST.Permissions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ERP.TEST.Students
{
    [Authorize(TESTPermissions.Students.Default)]
    public class StudentAppService: TESTAppService, IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        private readonly IRepository<Course, Guid> _courseRepository;

        public StudentAppService(IStudentRepository studentRepository, StudentManager studentManager,
           IRepository<Course, Guid> courseRepository)
        {
            _studentRepository = studentRepository;
            _studentManager = studentManager;
            _courseRepository = courseRepository;          
        }

        [Authorize(TESTPermissions.Students.Create), Authorize(TESTPermissions.Students.Edit)]
        public async Task<object> CreateAsync(CreateUpdateStudentDto input)
        {

            if (!await _studentRepository.AnyAsync(x => x.Id == input.Id))
            {
              return await _studentManager.CreateAsync(input.Name, input.Address, input.Age, input.Phone, input.CourseNames);
            }
            else
            {
                var student = await _studentRepository.GetAsync((Guid)input.Id, includeDetails: true);
              return  await _studentManager.UpdateAsync(student, input.Name, input.Address, input.Age, input.Phone, input.CourseNames);

            }



        }
        [Authorize(TESTPermissions.Students.Delete)]
        public async Task<string> DeleteAsync(Guid id)
        {
            var student=_studentRepository.GetAsync((Guid)id).Result;
            if(student==null)
            {
                return "This stydent Id:"+id+"  is invalid";
            }
            await _studentRepository.DeleteAsync(id,true);
            return "Student removed.";
        }
        public async Task<StudentDto> GetAsync(Guid id)
        {
            var student = await _studentRepository.GetAsync(id);
            return ObjectMapper.Map<StudentWithDetails, StudentDto>(student);

        }
        public async Task<PagedResultDto<StudentDto>> GetListAsync(StudentGetListInput input)
        {
            var students = await _studentRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount);
            var totalCount = await _studentRepository.CountAsync();

            return new PagedResultDto<StudentDto>(totalCount, ObjectMapper.Map<List<StudentWithDetails>, List<StudentDto>>(students));
        }
    
    }
}
