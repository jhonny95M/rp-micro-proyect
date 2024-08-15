/* eslint-disable @typescript-eslint/explicit-function-return-type */
import React from "react";
import { Button, Popover, Space, Switch, Tooltip } from "antd";
import { CopyOutlined, LinkOutlined } from "@ant-design/icons";
import dayjs from "dayjs";

export const columns = (onEnable: (item: any, status: boolean) => void) => [
  {
    title: "Usuario",
    key: "username",
    dataIndex: "username",
    render: (value: any, row: any) => {
      return (
        <div className="flex flex-row pr-2">
          <Popover
            content={() => {
              return (
                <div className="flex flex-row items-center">
                  <div className="flex-1 mr-2">ID: {row.id}</div>
                  <Button
                    shape="circle"
                    type="link"
                    icon={<CopyOutlined className="text-[--rp-bg]" />}
                  >
                    {" "}
                  </Button>
                </div>
              );
            }}
          >
            <LinkOutlined className="mr-2 text-[--rp-title]" />
          </Popover>
          <div className="flex-1">{value}</div>
        </div>
      );
    },
  },
  {
    title: "Email",
    key: "email",
    dataIndex: "email",
    render: (value: any, row: any) => {
      return (
        <div className="flex flex-row pr-2">
          <Popover
            content={() => {
              return (
                <div className="flex flex-row items-center">
                  <div className="flex-1 mr-2">ID: {row.id}</div>
                  <Button
                    shape="circle"
                    type="link"
                    icon={<CopyOutlined className="text-[--rp-bg]" />}
                  >
                    {" "}
                  </Button>
                </div>
              );
            }}
          >
            <LinkOutlined className="mr-2 text-[--rp-title]" />
          </Popover>
          <div className="flex-1">{value}</div>
        </div>
      );
    },
  },
  {
    title: "Password",
    key: "password",
    dataIndex: "password",
    render: (value: any, row: any) => {
      return (
        <div className="flex flex-row pr-2">
          <div className="flex-1">{value}</div>
        </div>
      );
    },
    width: 150,
  },
  {
    title: "Fecha de Nacimiento",
    key: "dateOfbirth",
    dataIndex: "dateOfbirth",
    render: (value: any, row: any) => {
      return dayjs(row.dateOfbirth ?? row.dateOfbirth).format("DD/MM/YYYY");
    },
    width: 150,
  },
  {
    title: "Estado",
    key: "status",
    dataIndex: "status",
    width: 100,
    render: (value: boolean, row: any) => {
      return (
        <Switch
          defaultChecked={value === true}
          checked={value === true}
          onChange={(evt) => {
            onEnable(row, evt);
          }}
        />
      );
    },
  },
];
