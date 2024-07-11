import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import { Layout, Menu, theme } from "antd";
import { Content, Header } from "antd/es/layout/layout";
import { Link, Outlet } from "react-router-dom";
import LogoutLink from "../Components/LogoutLink.tsx";

function Home() {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  return (
    <AuthorizeView>
      <span>
        <LogoutLink>
          Logout <AuthorizedUser value="email" />
        </LogoutLink>
      </span>
      <Layout>
        <Header style={{ display: "flex", alignItems: "center" }}>
          <div className="demo-logo" />
          <Menu
            theme="dark"
            defaultSelectedKeys={["1"]}
            mode="horizontal"
            style={{ flex: 1, minWidth: 0 }}
          >
            <Menu.Item key="1">
              <span>Events</span>
              <Link to="/events" />
            </Menu.Item>
            <Menu.Item key="2">
              <span>MyEvents</span>
              <Link to="/myevents" />
            </Menu.Item>
          </Menu>
        </Header>
        <Content className="contentContainer">
          <div
            style={{
              background: colorBgContainer,
              minHeight: 280,
              padding: 24,
              borderRadius: borderRadiusLG,
            }}
          >
            <Outlet />
          </div>
        </Content>
      </Layout>
    </AuthorizeView>
  );
}

export default Home;
