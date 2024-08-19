import React, { createContext, useContext, useEffect } from "react";
import { security, SecurityConfig } from "../config/securityConfig";

interface AuthContextType {
    user: {roles: string[]} | null;
    permissions: SecurityConfig;
    login:(roles:string[]) => void;
    logout:() => void;
}
const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{children: React.ReactNode }> = ({ children }) => {
    const [user, setUser] = React.useState<{roles: string[]} | null>(null);
    const [permissions, setPermissions] = React.useState<SecurityConfig>({});
    useEffect(() => {
        setPermissions(security as SecurityConfig);
}, []);
    const login = (roles: string[]) => setUser({roles});
    const logout = () => setUser(null);
    return (
        <AuthContext.Provider value={{ user,permissions, login, logout }}>
        {children}
        </AuthContext.Provider>
    );
};
export const useAuth = () => {
    const context = useContext(AuthContext);
    if(!context){
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
}