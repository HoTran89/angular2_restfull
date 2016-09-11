import {IModule, Module, MenuItem} from "../../../../common/models/layout";
import {Permissions} from "../../permission/permissions";
import {AuthenticationMode} from "../../../../common/enum";
import {PermissionsConstant} from "../../_share/common/constant";
import {AddPermissionConstant} from "../../_share/common/constant";
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
        { path: PermissionsConstant.Router, name: PermissionsConstant.Name, component: Permissions, data: { authentication: AuthenticationMode.Require }, useAsDefault: true },
        { path: AddPermissionConstant.Router, name: AddPermissionConstant.Name, component: AddPermission, data: { authentication: AuthenticationMode.Require } }
    ]);
    return module;
}