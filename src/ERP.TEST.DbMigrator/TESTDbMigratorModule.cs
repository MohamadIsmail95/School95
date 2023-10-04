using ERP.TEST.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ERP.TEST.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TESTEntityFrameworkCoreModule),
    typeof(TESTApplicationContractsModule)
    )]
public class TESTDbMigratorModule : AbpModule
{
}
