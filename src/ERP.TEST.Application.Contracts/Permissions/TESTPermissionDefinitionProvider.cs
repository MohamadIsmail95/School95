using ERP.TEST.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ERP.TEST.Permissions;

public class TESTPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TESTPermissions.MyPermission1, L("Permission:MyPermission1"));
        var shcoolGroup = context.AddGroup(TESTPermissions.GroupName, L("Permission:School"));

        var studentPermission = shcoolGroup.AddPermission(TESTPermissions.Students.Default, L("Permission:Students"));
        studentPermission.AddChild(TESTPermissions.Students.Create, L("Permission:Students.Create"));
        studentPermission.AddChild(TESTPermissions.Students.Edit, L("Permission:Students.Edit"));
        studentPermission.AddChild(TESTPermissions.Students.Delete, L("Permission:Students.Delete"));

        //---------------------------------------------------------------------------------------
        var coursePermission = shcoolGroup.AddPermission(TESTPermissions.Courses.Default, L("Permission:Courses"));

        coursePermission.AddChild(TESTPermissions.Courses.Create, L("Permission:Courses.Create"));
        coursePermission.AddChild(TESTPermissions.Courses.Edit, L("Permission:Courses.Edit"));
        coursePermission.AddChild(TESTPermissions.Courses.Delete, L("Permission:Courses.Delete"));



    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TESTResource>(name);
    }
}
