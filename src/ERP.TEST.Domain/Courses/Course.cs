using ERP.TEST.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ERP.TEST.Cources
{
    public class Course:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get;  set; }
        public virtual ICollection<StudentCourse> Students { get; set; }

        public Course() { }

        public Course(Guid id, string name,string description) : base(id)
        {
            SetName(name);
            Description = description;
        }

        public Course SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), CourseConsts.MaxNameLength);
            return this;
        }
    }
}
