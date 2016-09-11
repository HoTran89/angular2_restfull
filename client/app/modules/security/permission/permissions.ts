import {Component} from "angular2/core";
import { BasePage } from "../../../common/models/ui";
import { PermissionsModel } from './permissionsModel';
import {PageActions, Grid} from "../../../common/directive";
import {PageAction} from "../../../common/models/ui";
import permissionService from "../_share/services/permissionService";
import {Router} from "angular2/router";
import {AddPermissionConstant} from "../_share/common/constant";

@Component({
    templateUrl: "app/modules/security/permission/permissions.html",
    directives: [PageActions, Grid]
})

export class Permissions extends BasePage {
    public model: PermissionsModel = new PermissionsModel();
    public router: Router;
    constructor(router: Router) {
        super();
        let self: Permissions = this;
        self.router = router;
        self.model.addAction(new PageAction("btnAddPermission", "security.permissions.addPermissionAction",
            () => self.onAddPermissionClicked())
        )
        self.loadPermissions();
    }

    public onPermissionDeleteClicked(permission: any) {
        let self: Permissions = this;
        permissionService.deletePermission(permission.item.id).then(function () {
            self.loadPermissions();
        })
    }

    private loadPermissions() {
        let self = this;
        permissionService.getPermissions().then(function (permissions: Array<any>) {
            self.model.import(permissions);
        })
    }

    private onAddPermissionClicked() {
        this.router.navigate([AddPermissionConstant.Name]);
    }
}