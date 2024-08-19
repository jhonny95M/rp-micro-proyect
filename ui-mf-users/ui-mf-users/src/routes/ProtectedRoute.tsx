import React from "react";
import { Navigate, Outlet, Route, useLocation } from "react-router-dom";
import { usePermission } from "../Context/PermissionContext";

interface ProtectedRouteProps {
    requiredPermissions: string[];
}
const hasPermission = (userPermissions: string[], requiredPermissions: string[]) => 
    {
        console.log('userPermissions', userPermissions);
        console.log('requiredPermissions', requiredPermissions);
    
    return requiredPermissions.every(permission => userPermissions.includes(permission));
}
const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ requiredPermissions }) => {
    const { permissions } = usePermission();
    const location = useLocation();
    if(!permissions || !hasPermission(permissions, requiredPermissions)){
        return <Navigate to="/users" state={{ from: location.pathname }} />;
    }
    console.log('permissions', permissions);
    return <Outlet />;
}
export default ProtectedRoute;