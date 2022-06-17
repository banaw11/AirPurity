import { Type } from "@angular/core";

export interface ModalSettings{
 header : string;
 message? : string;
 isYesOrCancel : boolean;
 bodyTemplate: Type<unknown>;
 confirmActionName?: string;
}