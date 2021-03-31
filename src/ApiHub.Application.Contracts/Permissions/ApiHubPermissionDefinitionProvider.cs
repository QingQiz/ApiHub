using ApiHub.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ApiHub.Permissions
{
    public class ApiHubPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ApiHubPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ApiHubPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ApiHubResource>(name);
        }
    }
}
