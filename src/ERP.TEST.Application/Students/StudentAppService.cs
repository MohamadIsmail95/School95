using ERP.TEST.Cources;
using ERP.TEST.Courses;
using ERP.TEST.Permissions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace ERP.TEST.Students
{
    [Authorize(TESTPermissions.Students.Default)]
    public class StudentAppService: CrudAppService<Student, StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto, CreateUpdateStudentDto>,
        IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentManager _studentManager;
        private readonly IRepository<StudentCourse, Guid> _courceRepository;


        public StudentAppService(IRepository<Student, Guid> repository, IRepository<StudentCourse, Guid> courceRepository, IStudentRepository studentRepository,
             StudentManager studentManager) : base(repository)
        {
           
            _studentRepository = studentRepository;
            _studentManager = studentManager;
            _courceRepository = courceRepository;     

        }

     
        public override async Task<StudentDto> CreateAsync(CreateUpdateStudentDto input)
        {
            //Create New student
            if (!await _studentRepository.AnyAsync(x => x.Id == input.Id))
            {
                var student = await _studentManager.CreateAsync(input.Name, input.Address, input.Age, input.Phone, input.CoursesId);
                await _studentRepository.InsertAsync(student);
                return ObjectMapper.Map<Student, StudentDto>(student);

            }
            //Update students
            else
            {         
                    var student = await _studentRepository.GetAsync((Guid)input.Id);
                    var Upstudent = await _studentManager.EditAsync(student, input.Name, input.Address, input.Age, input.Phone, input.CoursesId);
                    await _courceRepository.DeleteAsync(x => x.StudentId == student.Id);
                    var newStudentCouses = Upstudent.Courses;
                    await _courceRepository.InsertManyAsync(newStudentCouses.ToList(),true);
                    await _studentRepository.UpdateAsync(Upstudent,true);                   
                    return ObjectMapper.Map<Student, StudentDto>(Upstudent);                         
            }



            }

        public override async Task<PagedResultDto<StudentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
           
            var students = await _studentRepository.GetListAsync(input.Sorting,input.SkipCount,input.MaxResultCount);
            var totalCount = await _studentRepository.CountAsync();

            return new PagedResultDto<StudentDto>(totalCount, ObjectMapper.Map<List<Student>, List<StudentDto>>(students));
        }

        public override async Task<StudentDto> GetAsync(Guid id)
        {
            var student = await _studentRepository.GetAsync(id);

            return ObjectMapper.Map<Student, StudentDto>(student);

        }

        public override async Task DeleteAsync(Guid id)
        {
            var IsExist = await _studentRepository.AnyAsync(x=>x.Id==id);
            if(!IsExist)
            {
                throw new UserFriendlyException(TESTDomainErrorCodes.StudentNotExists);

            }
            await _studentRepository.DeleteAsync(id);
        }

    }
}
