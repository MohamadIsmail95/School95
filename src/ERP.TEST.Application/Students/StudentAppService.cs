using ERP.TEST.Courses;
using ERP.TEST.Permissions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace ERP.TEST.Students
{
    public class StudentAppService: CrudAppService<Student, StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto, CreateUpdateStudentDto>,
        IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<StudentCourse, Guid> _courceRepository;


        public StudentAppService(IRepository<Student, Guid> repository, IRepository<StudentCourse, Guid> courceRepository, IStudentRepository studentRepository) : base(repository)
        {
            GetPolicyName = TESTPermissions.Students.Default;
            GetListPolicyName = TESTPermissions.Students.Default;
            CreatePolicyName = TESTPermissions.Students.Create;
            UpdatePolicyName = TESTPermissions.Students.Edit;
            DeletePolicyName = TESTPermissions.Students.Delete;
            _studentRepository = studentRepository;
            _courceRepository = courceRepository;
        }
 

        protected override async void MapToEntity(CreateUpdateStudentDto updateInput, Student entity)
        {
               
           await _courceRepository.DeleteAsync(x => x.StudentId==updateInput.Id);
            ObjectMapper.Map(updateInput, entity);
        }

        protected override async Task<IQueryable<Student>> CreateFilteredQueryAsync(PagedAndSortedResultRequestDto input)
        {
            return await _studentRepository.WithDetailsAsync();
        }
       

    }
}
