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
        Task<bool> FindByIdAsync(string name,Guid id);
    }
}
