import { Feature } from "./feature";
import { Profile } from "./profile";

export interface SecurityData {
    data: Profile[];
    features: Feature[];
    featuresTenant: any[];
    tenants: any[];
}