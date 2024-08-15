import { Layout } from "antd";

function MenuLayout() {
  return (
    <div>
      <Layout className="main-root-rp" >
        <Layout.Sider theme="light" width={200}>
          {/* <Menu /> */}
        </Layout.Sider>
        <Layout.Content></Layout.Content>
      </Layout>
    </div>
  );
}