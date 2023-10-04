using ERP.TEST.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ERP.TEST.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TESTController : AbpControllerBase
{
    protected TESTController()
    {
        LocalizationResource = typeof(TESTResource);
    }
}
