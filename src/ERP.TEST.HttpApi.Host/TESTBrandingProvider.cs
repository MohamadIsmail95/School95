using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ERP.TEST;

[Dependency(ReplaceServices = true)]
public class TESTBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TEST";
}
