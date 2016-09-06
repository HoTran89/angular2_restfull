using System.Collections.Generic;
using App.Common;
using App.Common.Data;
using App.Common.DI;
using App.Context;
using App.Repository.Security;
using App.Service.Security;

namespace App.Service.Impl.Security
{
    public class PermissionsService : IPermissionsService
    {
        public IList<PermissionListItem> GetPermissions()
        {
            using (UnitOfWork uow = new UnitOfWork(new AppDbContext()))
            {
                IPermissionsRepository permissionsRepository = IoC.Container.Resolve<IPermissionsRepository>();
                return permissionsRepository.GetItems<PermissionListItem>();
            }
        }

        public void DeletePermission(string id)
        {
            using (UnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionsRepository permissionsRepository = IoC.Container.Resolve<IPermissionsRepository>(uow);
                permissionsRepository.Delete(id);
                uow.Commit();
            }
        }
    }
}
