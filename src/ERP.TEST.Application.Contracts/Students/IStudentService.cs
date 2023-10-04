using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ERP.TEST.Students
{
    public interface IStudentService:IApplicationService
    {
        Task<PagedResultDto<StudentDto>> GetListAsync(StudentGetListInput input);
        Task<StudentDto> GetAsync(Guid id);
        Task<object> CreateAsync(CreateUpdateStudentDto input);
        Task<string> DeleteAsync(Guid id);

        
    }
}
