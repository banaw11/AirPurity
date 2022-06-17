import { NotificationSubject } from "./notificationSubject";

export class Notification{
    userEmail: string;
    cityId: number;
    stationId: number;
    indexLevelId: number | null;
    notificationSubjects : NotificationSubject[]

    public constructor(init?: Partial<Notification>){
        if(init){
            init.notificationSubjects = init.notificationSubjects?.filter(x => !isNaN(x.indexLevelId) && x.paramCode?.length > 0) ?? [];
            Object.assign(this, init);
        }
        else{
            this.notificationSubjects = [];
        }
    }
}