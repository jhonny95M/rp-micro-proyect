
import { createContext, useContext, useEffect, useState } from "react";
import { securityDataConfig } from "../types/securityConfig";
import { Menu } from "../types/menu";

interface PermissionContextType {
    permissions: string[];
    routes: string[];
    setPermissions: (permission: string[]) => void;
}
const PermissionContext = createContext<PermissionContextType|undefined>(undefined);

export const PermissionProvider: React.FC<{children: React.ReactNode}> = ({ children }) => {
    const [permissions, setPermissions] = useState<string[]>([]);
    const [routes, setRoutes] = useState<string[]>([]);

    const extractPermissionsAndRoutes = (menu: Menu) => {
        let actions: string[] = [];
        let routes: string[] = [];

        const traverseMenu = (menu: Menu) => {
            if (menu.route) {
                routes.push(menu.route);
            }
            if (menu.action) {
                actions.push(...menu.action.map(action => action.actionName));
            }
            if (menu.child) {
                menu.child.forEach(traverseMenu);
            }
        };

        traverseMenu(menu);
        return { actions, routes };
    };

    useEffect(() => {
        console.log('permisioncontext', securityDataConfig);
        const userPermissions = securityDataConfig.data[0].menu.flatMap((menu: Menu) => {
            const { actions, routes } = extractPermissionsAndRoutes(menu);
            setRoutes(prevRoutes => [...prevRoutes, ...routes]);
            return actions;
        });
        console.log('userPermissions', userPermissions);
        setPermissions(userPermissions);
    }, []);
    // const hasPermission = (permission: string[]) => {
    //     return permission.every(p => permissions.includes(p));
    // }
    return (
        <PermissionContext.Provider value={{ permissions, routes, setPermissions }}>
            {children}
        </PermissionContext.Provider>
    );
};
export const usePermission = () => {
    const context = useContext(PermissionContext);
    if(!context){
        throw new Error('usePermission must be used within an PermissionProvider');
    }
    return context;
}