using ERP.TEST.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ERP.TEST.Courses
{
    public interface IStudentCourseRepository: IRepository<StudentCourse>
    {
    }
}
