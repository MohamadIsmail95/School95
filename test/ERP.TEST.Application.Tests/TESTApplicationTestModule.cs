using Volo.Abp.Modularity;

namespace ERP.TEST;

[DependsOn(
    typeof(TESTApplicationModule),
    typeof(TESTDomainTestModule)
    )]
public class TESTApplicationTestModule : AbpModule
{

}
