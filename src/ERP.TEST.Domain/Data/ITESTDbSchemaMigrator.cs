using System.Threading.Tasks;

namespace ERP.TEST.Data;

public interface ITESTDbSchemaMigrator
{
    Task MigrateAsync();
}
