using System.Collections.Generic;
using App.Entity.Security;

namespace App.Service.Security
{
    public interface IPermissionsService
    {
        IList<PermissionListItem> GetPermissions();
        void DeletePermission(string id);
        void AddPermission(AddPermissionRequest request);
        Permission GetPermissionByName(string name);
    }
}
