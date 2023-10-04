using Volo.Abp.Settings;

namespace ERP.TEST.Settings;

public class TESTSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TESTSettings.MySetting1));
    }
}
