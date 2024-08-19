import React, { useRef } from "react";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/home/Home";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ErrorBoundary } from "react-error-boundary";
import Error from "./pages/error/Index";
import { App, ConfigProvider } from "antd";
import { UserMain } from "./pages/user/main/Index";

import "./index.css";
import { RouteProvider } from "./Context/RouteContext";
import { AuthProvider } from "./Context/AuthProvider";
import ProtectedRoute from "./routes/ProtectedRoute";
import { PermissionProvider } from "./Context/PermissionContext";

const queryClient = new QueryClient();

export default function Root(): React.ReactNode {
  const modalContainerRef = useRef(null);

  return (
    <ErrorBoundary
      FallbackComponent={Error}
      onError={(error, info) => {
        console.error(error);
        console.error(info);
      }}
      onReset={(details) => {
        window.location.reload();
      }}
    >
      <ConfigProvider
        theme={{
          token: {
            // Seed Token
            borderRadius: 8,
            colorHighlight:
              "linear-gradient(90deg, rgba(127, 0, 255, 1) 0%, rgba(154, 59, 250, 1) 100%)",
            colorPrimary:
              "linear-gradient(90deg, rgba(127, 0, 255, 1) 0%, rgba(154, 59, 250, 1) 100%)",
          },
        }}
        getPopupContainer={() => modalContainerRef?.current as unknown as HTMLElement}
      >
        <App>
          <div ref={modalContainerRef}>
              {/* <PermissionProvider> */}
            <QueryClientProvider client={queryClient}>
                <RouteProvider>
                  <Routes>
                    <Route index element={<Home />} />
                    <Route path="/" element={<Home />} />
                      <Route path="/users" element={<UserMain />} />
                    {/* <Route element={<ProtectedRoute requiredPermissions={['edit-user','create-user']}  />}>
                    </Route> */}
                    <Route path="/japi/inmobiliario" element={<Home />} />
                    {/* <Route element={<ProtectedRoute requiredPermissions={['edit-user','create-user']} />}> */}
                      <Route path="/private" element={<h1>Private</h1>}  />
                    {/* </Route> */}
                  </Routes>
                </RouteProvider>
            </QueryClientProvider>
              {/* </PermissionProvider> */}
          </div>
        </App>
      </ConfigProvider>
    </ErrorBoundary>
  );
}
