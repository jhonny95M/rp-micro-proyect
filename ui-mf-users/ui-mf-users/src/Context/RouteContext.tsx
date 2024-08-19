import { createContext, useContext } from "react";
import { useLocation } from "react-router";

const RouteContext = createContext<string | null>(null);

export const RouteProvider: React.FC<{children: React.ReactNode }> = ({ children }) => {
    const location = useLocation();
    return <RouteContext.Provider value={location.pathname}>{children}</RouteContext.Provider>;
}

export const useRoute = () => {
    const context = useContext(RouteContext);
    if(!context){
        throw new Error('useRoute must be used within an RouteProvider');
    }
    return context;
}