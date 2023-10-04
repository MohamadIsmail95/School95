using ERP.TEST.Cources;
using ERP.TEST.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<StudentWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();

            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(StudentWithDetails.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<StudentWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();

            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        private async Task<IQueryable<StudentWithDetails>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync())
                .Include(x => x.Courses)
                .Select(x => new StudentWithDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address=x.Address,
                    Phone=x.Phone,
                    Age=x.Age,
                    CourseNames = (from studentCourse in x.Courses
                                     join course in dbContext.Set<Course>() on studentCourse.CourseId equals course.Id
                                     select course.Name).ToArray()
                });
        }

        public override Task<IQueryable<Student>> WithDetailsAsync()
        {
            return base.WithDetailsAsync(x => x.Courses);
        }
    }
}

