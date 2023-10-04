using ERP.TEST.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ERP.TEST;

[DependsOn(
    typeof(TESTEntityFrameworkCoreTestModule)
    )]
public class TESTDomainTestModule : AbpModule
{

}
