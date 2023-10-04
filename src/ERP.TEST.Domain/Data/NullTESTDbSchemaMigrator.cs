using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ERP.TEST.Data;

/* This is used if database provider does't define
 * ITESTDbSchemaMigrator implementation.
 */
public class NullTESTDbSchemaMigrator : ITESTDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
