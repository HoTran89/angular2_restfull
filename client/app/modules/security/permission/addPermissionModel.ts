import {ValidationException} from "../../../common/models/exception";

export class AddPermissionModel {
    public name: string = String.empty;
    public key: string = String.empty;
    public description: string = String.empty;

    public Invalid(): boolean {
        let validationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            validationException.add("security.addPermission.nameIsRequired");
        }
        if (String.isNullOrWhiteSpace(this.key)) {
            validationException.add("security.addPermission.keyIsRequired");
        }
        validationException.throwIfHasError();
        return validationException.hasError();
    }
}