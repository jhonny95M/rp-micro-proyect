// import React, { useEffect, useState } from "react";
// import { Route, Routes } from "react-router-dom";
// import { RpLayout } from "web-ui-design";
// import { useLocation, useNavigate } from "react-router-dom";
import { identityUtil } from "web-utilities";
import loading from "../src/assets/img/header-logo.svg";
// import "./index.css";
// import { ErrorBoundary } from "react-error-boundary";
// import Error from "./components/Error/Error";
import React, { useState } from 'react';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UploadOutlined,
  UserOutlined,
  VideoCameraOutlined,
} from '@ant-design/icons';
import { Button, Layout, Menu, theme } from 'antd';

const { Header, Sider, Content } = Layout;


const Identity = React.lazy(() => import("Identity/Root"));
const UsersModule = React.lazy(() => import("usersModule/Root"));

export const App = (props: any) => {

  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();


  
  // const location = useLocation();
  // const navigate = useNavigate();
  // const [splash, setSplash] = useState(true);

  // useEffect(() => {
  //   const timeoutId = setTimeout(() => {
  //     setSplash(false);
  //   }, 1000);
  //   return () => clearTimeout(timeoutId);
  // }, []);

  React.useEffect(() => {
    document.getElementById("body")?.classList?.remove("background-rp");
  }, []);

  const fallback = () => {
    return (
      <div className="splash-rp">
        <img src={loading} width={150}></img>
      </div>
    );
  };

  if (!identityUtil.isAuthenticated || localStorage.getItem("logout") != null) {
    return <Identity />;
  } else {
    return (
      <>
      {/* <RpLayout
        {...props}
        location={location}
        navigate={navigate}
        content={() => {
          return (
            <>
              <ErrorBoundary
                FallbackComponent={Error}
                onError={(error, info) => {
                  debugger;
                  console.error(error);
                  console.error(info);
                }}
                onReset={(details) => {
                  debugger;
                  window.location.reload();
                }}
              >
                {splash && fallback()}  
                <React.Suspense fallback={fallback()}>
                  <Routes>
                  <Route path="/dashbooard" element={<h1>Hello world!</h1>}    />
                  <Route path="/analisis" element={<h1>Analisis!</h1>}    />
                  <Route path="test-app-core/*" element={<UsersModule />}   />
                  </Routes>
                </React.Suspense>
              </ErrorBoundary>
            </>
          );
        }}
      >
        
      </RpLayout> */}
      <Layout>
      <Sider trigger={null} collapsible collapsed={collapsed}>
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['1']}
          items={[
            {
              key: '1',
              icon: <UserOutlined />,
              label: 'nav 1',
            },
            {
              key: '2',
              icon: <VideoCameraOutlined />,
              label: 'nav 2',
            },
            {
              key: '3',
              icon: <UploadOutlined />,
              label: 'nav 3',
            },
          ]}
        />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }}>
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            style={{
              fontSize: '16px',
              width: 64,
              height: 64,
            }}
          />
        </Header>
        <Content
          style={{
            margin: '24px 16px',
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
          }}
        >
          Content
        </Content>
      </Layout>
    </Layout>
      </>
    );
  }
};
