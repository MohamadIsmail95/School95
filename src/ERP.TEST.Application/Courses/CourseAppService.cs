using ERP.TEST.Cources;
using ERP.TEST.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ERP.TEST.Courses
{
    public class CourseAppService:CrudAppService<Course,CourseDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCourseDto, CreateUpdateCourseDto>,
        ICourseAppService
    {
        public CourseAppService(IRepository<Course, Guid> repository) : base(repository)
        {
            GetPolicyName = TESTPermissions.Courses.Default;
            GetListPolicyName = TESTPermissions.Courses.Default;
            CreatePolicyName = TESTPermissions.Courses.Create;
            UpdatePolicyName = TESTPermissions.Courses.Edit;
            DeletePolicyName = TESTPermissions.Courses.Delete;
        }
    }
}
