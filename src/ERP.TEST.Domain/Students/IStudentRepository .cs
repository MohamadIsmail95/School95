using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ERP.TEST.Students
{
    public interface IStudentRepository :IRepository<Student,Guid>
    {
        Task<Student> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Student>> GetListAsync(string sorting, int skipCount, int maxResultCount);
        Task<bool> FindByIdAsync(string name,Guid id);

    }
}
