using ERP.TEST.Cources;
using ERP.TEST.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ERP.TEST.Students
{
    public class EfCoreStudentRepository: EfCoreRepository<TESTDbContext, Student, Guid>, IStudentRepository
    {
        public EfCoreStudentRepository(IDbContextProvider<TESTDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }

        public   async   Task<Student> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var db = await GetDbSetAsync();

            return await db.AsNoTracking()
                .Where(x => x.Id == id).Include(x=>x.RelationCourses).Include(x=>x.Courses)
                .FirstOrDefaultAsync();
        }

        public  async   Task<List<Student>> GetListAsync(string sorting, int skipCount,int maxResultCount)

        {
            var db = await GetDbSetAsync();
            return await db.Include(x => x.RelationCourses)
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Student.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(default));
        }

        public async Task<bool> FindByIdAsync(string name,Guid id)
        {
            var db = await GetDbSetAsync();
            var isUniqueName = await db.AnyAsync(x => x.Name == name && x.Id != id);
            return isUniqueName;


        }
    }
}

