import securityConfig from '../data/security.json';
export interface SecurityConfig {
    [key: string]: {
        roles: string[];
        children?:SecurityConfig;
    };
}
export const security: SecurityConfig | unknown = securityConfig;