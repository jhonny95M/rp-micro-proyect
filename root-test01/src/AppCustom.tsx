import React, { useState } from 'react';
import {
  DesktopOutlined,
  FileOutlined,
  PieChartOutlined,
  TeamOutlined,
  UserOutlined,
} from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { Route, Routes, useNavigate } from 'react-router-dom';
import menuData from './data/security.json';
import loading from "../src/assets/img/header-logo.svg";

const { Header, Content, Footer, Sider } = Layout;

type MenuItem = Required<MenuProps>['items'][number];

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
): MenuItem {
  return {
    key,
    icon,
    children,
    label
  } as MenuItem;
}

const transformMenuData = (menuData: any[]): {items:MenuItem[],routes: Record<string,string>} => {
    console.log(menuData);
    let routes:Record<string,string> = {};
    const items= menuData.map((item) => {
        const children = item.child ? transformMenuData(item.child) :{items:[], routes:{}};
        console.log('route item '+item.menuId + '-'+ item.route);
        routes[item.menuId] = item.route;
        routes = { ...routes, ...children.routes };
        if(children.items.length==0)
            return getItem(item.label, item.menuId);
        else
            return getItem(item.label, item.menuId, undefined, children.items);
    });
    return {items, routes};
}

const {items:menuItems, routes: menuRoutes} = transformMenuData(menuData.data[0].menu);

const items: MenuItem[] = [
  getItem('Option 1', '1', <PieChartOutlined />),
  getItem('Option 2', '2', <DesktopOutlined />),
  getItem('User', 'sub1', <UserOutlined />, [
    getItem('Tom', '3'),
    getItem('Bill', '4'),
    getItem('Alex', '5'),
  ]),
  getItem('Team', 'sub2', <TeamOutlined />, [getItem('Team 1', '6'), getItem('Team 2', '8')]),
  getItem('Files', '9', <FileOutlined />),
];

const UsersModule = React.lazy(() => import("usersModule/Root"));
const AppCustom: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  const navigate = useNavigate();

  const onMenuClick = (e: {key:React.Key}) => {
    console.log('click ', e);
    const route = menuRoutes[e.key.toString()];
    console.log(route);
    if(route) navigate(route);
  };
  const fallback = () => {
    return (
      <div className="splash-rp">
        <img src={loading} width={150}></img>
      </div>
    );
  };
  console.log(menuItems);
  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider collapsible collapsed={collapsed} onCollapse={(value) => setCollapsed(value)}>
        <div className="demo-logo-vertical" />
        <Menu 
        theme="dark" 
        defaultSelectedKeys={['1']}
        mode="inline" 
        items={menuItems} 
        onClick={onMenuClick} />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }} />
        <Content style={{ margin: '0 16px' }}>
          <Breadcrumb style={{ margin: '16px 0' }}>
            <Breadcrumb.Item>User</Breadcrumb.Item>
            <Breadcrumb.Item>Bill</Breadcrumb.Item>
          </Breadcrumb>
          <div
            style={{
              padding: 24,
              minHeight: 360,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
            }}
          >
            Bill is a cat.
            <React.Suspense fallback={fallback()}>
            <Routes>
                      <Route path="/dashbooard" element={<h1>Hello world!</h1>}    />
                      <Route path="/analisis" element={<h1>Analisis!</h1>}    />
                      <Route path="maintenance/*" element={<UsersModule />}   />
                    </Routes>
                    </React.Suspense>
          </div>
        </Content>
        <Footer style={{ textAlign: 'center' }}>
          Ant Design ©{new Date().getFullYear()} Created by Ant UED
        </Footer>
      </Layout>
    </Layout>
  );
};

export default AppCustom;