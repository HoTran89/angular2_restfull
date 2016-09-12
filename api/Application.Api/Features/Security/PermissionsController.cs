using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using App.Common.DI;
using App.Common.Http;
using App.Common.Validation;
using App.Entity.Security;
using App.Service.Security;

namespace App.Api.Features.Security
{
    [RoutePrefix("api/permissions")]
    public class PermissionsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IResponseData<IList<PermissionListItem>> GetPermissions()
        {
            IResponseData<IList<PermissionListItem>> responseData = new ResponseData<IList<PermissionListItem>>();
            try
            {
                IPermissionsService permissionsService = IoC.Container.Resolve<IPermissionsService>();
                IList<PermissionListItem> items = permissionsService.GetPermissions();
                responseData.SetData(items);
            }
            catch (ValidationException exception)
            {
                responseData.SetErrors(exception.Errors);
                responseData.SetStatus(HttpStatusCode.PreconditionFailed);
            }
            return responseData;
        }

        [HttpDelete]
        [Route("{id}")]
        public IResponseData<string> DeletePermission([FromUri] string id)
        {
            IResponseData<string> responseData = new ResponseData<string>();
            try
            {
                IPermissionsService permissionService = IoC.Container.Resolve<IPermissionsService>();
                permissionService.DeletePermission(id);
            }
            catch (ValidationException exception)
            {
                responseData.SetErrors(exception.Errors);
                responseData.SetStatus(HttpStatusCode.PreconditionFailed);
            }
            return responseData;
        }

        [HttpPost]
        [Route("")]
        public IResponseData<Permission> AddPermission([FromBody] AddPermissionRequest request)
        {
            IResponseData<Permission> responseData = new ResponseData<Permission>();
            try
            {
                IPermissionsService permissionsService = IoC.Container.Resolve<IPermissionsService>();
                permissionsService.AddPermission(request);
                Permission permission = permissionsService.GetPermissionByName(request.Name);
                responseData.SetData(permission);
            }
            catch (ValidationException exception)
            {
                responseData.SetErrors(exception.Errors);
                responseData.SetStatus(HttpStatusCode.PreconditionFailed);
            }
            return responseData;
        }
    }
}