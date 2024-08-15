import React from "react";
import { Typography } from "antd";
import "./styles.css";

const Home: React.FC = () => {
  return (
    <section className="home main-container">
      <Typography.Title level={2}>
        Bienvenido a la pantalla principal de Test App!
      </Typography.Title>
    </section>
  );
};

export default Home;
