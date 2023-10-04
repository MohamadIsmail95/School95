using System;
using System.Collections.Generic;
using System.Text;
using ERP.TEST.Localization;
using Volo.Abp.Application.Services;

namespace ERP.TEST;

/* Inherit your application services from this class.
 */
public abstract class TESTAppService : ApplicationService
{
    protected TESTAppService()
    {
        LocalizationResource = typeof(TESTResource);
    }
}
