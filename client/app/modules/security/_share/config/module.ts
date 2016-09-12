import {IModule, Module, MenuItem} from "../../../../common/models/layout";
import {Permissions} from "../../permission/permissions";
import {AuthenticationMode} from "../../../../common/enum";
import {permissionsConstant} from "../../_share/common/constant";
import {addPermissionConstant} from "../../_share/common/constant";
import {AddPermission} from "../../permission/addPermission";

let umModule: IModule = createModule();
export default umModule;
function createModule() {
    let module = new Module("app/modules/security", "security");
    module.menus.push(
        new MenuItem(
            "Security", "/Permissions", "fa fa-users",
            new MenuItem("Permissions", "/Permissions", "")
        )
    );
    module.addRoutes([
        { path: permissionsConstant.Route, name: permissionsConstant.Name, component: Permissions, data: { authentication: AuthenticationMode.Require }, useAsDefault: true },
        { path: addPermissionConstant.Route, name: addPermissionConstant.Name, component: AddPermission, data: { authentication: AuthenticationMode.Require } }
    ]);
    return module;
}