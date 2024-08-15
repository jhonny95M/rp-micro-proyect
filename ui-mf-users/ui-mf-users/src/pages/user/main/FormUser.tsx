import React, { useEffect, useState } from "react";
import { DateTimePicker, FormItem, FormRP, Input, Select } from "web-ui-design";
import { localizedUI } from "../../../shared/constants/strings";
import { required, maxLength, email } from "../../../shared/constants/validators";
import { UseFormReturn, useForm } from "react-hook-form";
import { IUser } from "../../../shared/types/user.types";
import dayjs from "dayjs";
import { Select as SelectOption, Switch } from "antd";
import { IRol } from "ui-mf-users/src/shared/types/rol.types";

interface IUserFormProps {
  initialData?: IUser;
  comboData?: IRol[];
  onChangeForm?: (form?: UseFormReturn<any, any, any>) => void;
}
export type ConnectorProps = React.PropsWithChildren<IUserFormProps>;

export const FormUserContent = (props: ConnectorProps): React.ReactElement | null => {
  const [model, setModel] = useState<IUser>({});
  const form = useForm<any, any, any>({ defaultValues: model ?? {} });

  const {
    formState: { errors },
  } = form;

  useEffect(() => {
    setModel(props.initialData?? {});
  }, [props.initialData]);

  useEffect(() => {
    form.reset(model);
  }, [model]);

  return (
    <FormRP form={form} onChange={props.onChangeForm} >
      <FormItem errors={errors.username} label="Nombre">
        <Input
          name="username"
          allowClear
          formRP={form}
          defaultValue={model.username}
          rules={{ ...required(), ...maxLength(200) }}
          placeholder={localizedUI.PlaceHolderInput}
        />
      </FormItem>
      <FormItem errors={errors.email} label="Email">
        <Input
          name="email"
          allowClear
          formRP={form}
          type="email"
          defaultValue={model.email}
          rules={{ ...required(), ...maxLength(200), ...email()  }}
          
          placeholder={localizedUI.PlaceHolderInput}
        />
      </FormItem>
      <FormItem errors={errors.password} label="Contraseña">
        <Input
          name="password"
          allowClear
          formRP={form}
          type="password"
          defaultValue={model.password}
          rules={{ ...required(), ...maxLength(200) }}
          placeholder={localizedUI.PlaceHolderInput}
        />
        </FormItem>
       
      <FormItem errors={errors.dateOfbirth} label="Fecha Nacimiento">
        <Input
          name="dateOfbirth"
          allowClear
          formRP={form}
          type="date"
          defaultValue={model.dateOfbirth ? dayjs(model.dateOfbirth).format("YYYY-MM-DD"):""}
          // value={model.dateOfbirth ? dayjs(model.dateOfbirth).format():""}
          rules={{ ...required() }}
          placeholder={localizedUI.PlaceHolderInput}
        />
      </FormItem>
      
      <FormItem errors={errors.roleId} label="Rol">
        <Select
          name="roleId"
          allowClear
          formRP={form}
          value={model.roleId?.toString()}
          rules={{ ...required() }}
          placeholder={localizedUI.PlaceHolderSelect}
        >
        {props.comboData?.map((x) => (
          <SelectOption.Option key={x.id} value={x.id}>
            {x.namerol?.toUpperCase()}
          </SelectOption.Option>
        ))}
        {/* <SelectOption.Option   value={1}>Admin</SelectOption.Option> */}
        </Select>
        </FormItem>
        <FormItem label="Estado" errors={errors.status} className="flex items-center justify-between">
          <Switch className="ml-4"
            defaultChecked={model.status}
            checked={model.status}            
            onChange={(evt) => {
              console.log(evt);
              model.status = evt;
              form.setValue("status", evt);
            }}
          />
        </FormItem>
    </FormRP>
  );
};