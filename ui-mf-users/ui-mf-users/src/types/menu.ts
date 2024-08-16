import { Action } from "./Action";

export interface Menu{
    menuId: string;
    menuParentId: string;
    menuNivel:string;
    label:string;
    icon:string;
    route:string;
    flatModal:string;
    status:string;
    child:Menu[];
    action:Action[];
}
