using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace ERP.TEST.Students
{
    public class StudentAlreadyExistsException: BusinessException
    {
        public StudentAlreadyExistsException(string name)
       : base(TESTDomainErrorCodes.StudentAlreadyExists)
        {
            WithData("name", name);

        }
    }
}
