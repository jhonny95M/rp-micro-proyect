import React, { useRef } from "react";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/home/Home";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ErrorBoundary } from "react-error-boundary";
import Error from "./pages/error/Index";
import { App, ConfigProvider } from "antd";
import { UserMain } from "./pages/user/main/Index";

import "./index.css";

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
            <QueryClientProvider client={queryClient}>
              <Routes>
                <Route index element={<Home />} />
                <Route path="/" element={<Home />} />
                <Route path="/users" element={<UserMain />} />
              </Routes>
            </QueryClientProvider>
          </div>
        </App>
      </ConfigProvider>
    </ErrorBoundary>
  );
}
