import { Menu } from "./menu";

export interface Profile{
    codeProfile: string;
    code:string;
    id:string;
    nameProfile:string;
    statusProfile:string;
    isMaintenant:boolean;
    tenantId:string;
    tenantProfile:string;
    tenantCode:string;
    menu:Menu[];
}