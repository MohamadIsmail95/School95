using ERP.TEST.Cources;
using ERP.TEST.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
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

        public override async Task<IQueryable<Student>> WithDetailsAsync()
        {
            var db = await GetDbSetAsync();

            return  db.Include(x => x.RelationCourses).Include(x => x.Courses).AsQueryable();

        }

        
    }
}

