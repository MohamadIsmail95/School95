using ERP.TEST.Cources;
using ERP.TEST.Courses;
using ERP.TEST.Localization;
using ERP.TEST.Permissions;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace ERP.TEST.Students
{
    public class StudentAppService: CrudAppService<Student, StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto, CreateUpdateStudentDto>,
        IStudentAppService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRepository<StudentCourse, Guid> _studentCourceRepository;
        private readonly IRepository<Course, Guid> _courceRepository;
        private readonly IStringLocalizer<TESTResource> _localizer;


        public StudentAppService(IRepository<Student, Guid> repository, IRepository<StudentCourse, Guid> studentCourceRepository, IStudentRepository studentRepository,
          IRepository<Course, Guid> courceRepository, IStringLocalizer<TESTResource> localizer) : base(repository)
        {
            GetPolicyName = TESTPermissions.Students.Default;
            GetListPolicyName = TESTPermissions.Students.Default;
            CreatePolicyName = TESTPermissions.Students.Create;
            UpdatePolicyName = TESTPermissions.Students.Edit;
            DeletePolicyName = TESTPermissions.Students.Delete;
            _studentRepository = studentRepository;
            _studentCourceRepository = studentCourceRepository;
            _courceRepository= courceRepository;
            _localizer= localizer;
        }

        protected override async  Task<Student> MapToEntityAsync(CreateUpdateStudentDto createInput)
        {
            if(await _studentRepository.AnyAsync(x=>x.Name==createInput.Name && x.Id!=createInput.Id))
            {
                throw new UserFriendlyException(_localizer["StudentAreadyExsist"]);
            }

            var courses = await _courceRepository.GetListAsync(x => createInput.CoursesId.Contains(x.Id));
            if (courses.Count!=createInput.CoursesId.Count())
            {
                throw new UserFriendlyException(_localizer["CourseNotFoundMessage"]);

            }

            return await Task.FromResult(MapToEntity(createInput));
        }

        protected override async Task MapToEntityAsync(CreateUpdateStudentDto updateInput, Student entity)
        {
            if (await _studentRepository.AnyAsync(x => x.Name == updateInput.Name && x.Id != updateInput.Id))
            {
                throw new UserFriendlyException(_localizer["StudentAreadyExsist"]);
            }

            var courses = await _courceRepository.GetListAsync(x => updateInput.CoursesId.Contains(x.Id));
            if (courses.Count != updateInput.CoursesId.Count())
            {
                throw new UserFriendlyException(_localizer["CourseNotFoundMessage"]);

            }

            await _studentCourceRepository.DeleteAsync(x => x.StudentId==updateInput.Id);
            MapToEntity(updateInput, entity);
        }

        protected override async Task<IQueryable<Student>> CreateFilteredQueryAsync(PagedAndSortedResultRequestDto input)
        {
            return await _studentRepository.WithDetailsAsync();
        }
            

    }
}
