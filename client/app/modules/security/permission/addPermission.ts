import {Component} from "angular2/core";
import {BasePage} from "../../../common/models/ui";
import {Router} from "angular2/router";
import {PermissionsConstant} from "../_share/common/constant";
import permissionService from "../_share/services/permissionService";
import {AddPermissionModel} from "./addPermissionModel";
import {ValidationDirective} from "../../../common/directive";

@Component({
    templateUrl: "app/modules/security/permission/addPermission.html",
    directives: [ValidationDirective]
})

export class AddPermission extends BasePage {
    public model: AddPermissionModel = new AddPermissionModel();
    public router: Router;

    constructor(router: Router) {
        super();
        let self: AddPermission = this;
        self.router = router;
    }

    public onCancelClicked() {
        this.router.navigate([PermissionsConstant.Name]);
    }

    public onSaveClicked() {
        let self: AddPermission = this;
        if (self.model.Invalid()) {
            return;
        }
        permissionService.addPermission(self.model).then(function () {
            self.router.navigate([PermissionsConstant.Name]);
        })
    }
}