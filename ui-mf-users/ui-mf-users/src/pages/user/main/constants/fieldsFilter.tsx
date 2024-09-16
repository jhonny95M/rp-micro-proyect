import React from "react";
import { Input, Select } from "web-ui-design";
import { CaretDownOutlined } from "@ant-design/icons";
import { localizedUI } from "../../../../shared/constants/strings";
import { Select as SelectANTD } from "antd";

export const fieldsFilter = [
  {
    name: "searchFilter",
    label: "Búsqueda",
    component: <Input allowClear placeholder={localizedUI.PlaceHolderInput} />,
  },
  {
    name: "status",
    label: "Estado",
    component: (
      <Select
        allowClear
        placeholder={localizedUI.PlaceHolderSelect}
        suffixIcon={<CaretDownOutlined />}
      >
        <SelectANTD.Option key={1} value={1}>
          Habilitado
        </SelectANTD.Option>
        <SelectANTD.Option key={0} value={0}>
          Deshabilitado
        </SelectANTD.Option>
      </Select>
    ),
  },
];
