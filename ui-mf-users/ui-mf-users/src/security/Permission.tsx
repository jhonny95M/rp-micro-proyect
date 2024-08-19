import React from "react";
import { usePermission } from "../Context/PermissionContext";

interface PermissionProps {
    requeridPermissions: string[];
}
const Permission: React.FC<{requeridPermissions: string[], children: React.ReactNode}> = ({ requeridPermissions, children }) => {
    const { permissions } = usePermission();
    
    if (requeridPermissions.every((permission:string) => permissions.includes(permission))) {
        return <>{children}</>;
    }
    return null;
}
export default Permission;