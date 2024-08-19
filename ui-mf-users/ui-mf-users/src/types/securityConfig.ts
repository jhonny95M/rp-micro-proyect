import securityConfigData from '../data/security.json';
import { Feature } from "./feature";
import { Profile } from "./profile";

export interface SecurityConfig {
    data: Profile[];
    features: Feature[];
    featuresTenant: any[];
    tenants: any[];
}
export const securityDataConfig: SecurityConfig | any = securityConfigData;