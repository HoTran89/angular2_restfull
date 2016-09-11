using System;
using System.Collections.Generic;
using App.Common;
using App.Common.Data;
using App.Common.DI;
using App.Common.Validation;
using App.Context;
using App.Entity.Security;
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

        public void AddPermission(AddPermissionRequest request)
        {
            ValidationAddPermission(request);
            using (UnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionsRepository permissionsRepository = IoC.Container.Resolve<IPermissionsRepository>(uow);
                Permission permission = new Permission(request.Name, request.Key, request.Description);
                permissionsRepository.Add(permission);
                uow.Commit();
            }
        }

        private void ValidationAddPermission(AddPermissionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("security.addPermission.nameIsRequired");
            }
            if (GetPermissionByName(request.Name) != null)
            {
                throw new ValidationException("security.addPermission.nameIsAlreadyExist");
            }
            if (string.IsNullOrWhiteSpace(request.Key))
            {
                throw new ValidationException("security.addPermission.keyIsRequired");
            }
            if (GetPermissionByKey(request.Key) != null)
            {
                throw new ValidationException("security.addPermission.keyIsAlreadyExist");
            }
        }

        private Permission GetPermissionByKey(string key)
        {
            using (UnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Read)))
            {
                IPermissionsRepository permissionsRepository = IoC.Container.Resolve<IPermissionsRepository>(uow);
                return permissionsRepository.GetByKey(key);
            }
        }

        public Permission GetPermissionByName(string name)
        {
            using (UnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Read)))
            {
                IPermissionsRepository permissionsRepository = IoC.Container.Resolve<IPermissionsRepository>(uow);
                return permissionsRepository.GetByName(name);
            }
        }
    }
}
