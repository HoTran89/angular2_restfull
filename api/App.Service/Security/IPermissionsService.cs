using System.Collections.Generic;

namespace App.Service.Security
{
    public interface IPermissionsService
    {
        IList<PermissionListItem> GetPermissions();
        void DeletePermission(string id);
    }
}
