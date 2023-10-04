using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace ERP.TEST.Students
{
    public class StudentWithDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string[] CourseNames { get; set; }
        //public DateTime CreationTime { get; set; }
    }
}
