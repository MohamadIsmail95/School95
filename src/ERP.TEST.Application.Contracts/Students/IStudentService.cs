using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TEST.Students
{
    public interface IStudentService: ICrudAppService<StudentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStudentDto, CreateUpdateStudentDto>
    {
        Task<PagedResultDto<StudentDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<StudentDto> GetAsync(Guid id);
        Task<StudentDto> CreateAsync(CreateUpdateStudentDto input);
        Task DeleteAsync(Guid id);
    }
}
