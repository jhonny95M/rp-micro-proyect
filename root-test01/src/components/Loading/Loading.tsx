import React from "react";
import "./Loading.css";
import { LoadingOutlined } from "@ant-design/icons";

export default function Loading(props: any) {
  return (
    <div
      id="login-components-loading"
      style={props.styleContainer??{}}
    >
      <LoadingOutlined
        className={
          props.size !== undefined
            ? props.size + " image-loading"
            : "default image-loading"
        }
        style={{...props.style}}
      />
      {props.children}
    </div>
  );
}
