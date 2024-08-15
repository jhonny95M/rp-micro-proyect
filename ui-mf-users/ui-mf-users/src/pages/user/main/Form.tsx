/* eslint-disable prettier/prettier */
/* eslint-disable @typescript-eslint/no-misused-promises */
import React, { useCallback, useEffect, useState } from "react";
import { Button, Modal, Space, notification } from "antd";
import {
  Loading,
  Container,
} from "web-ui-design";
import { useMutation, useQuery, identityUtil } from "web-utilities";
import { type FieldValues, type UseFormReturn } from "react-hook-form";
import { localizedOps, localizedUI } from "../../../shared/constants/strings";
import { buildMSUrl } from "../../../shared/constants/urls";
import { IUser } from "../../../shared/types/user.types";
import { IFormProps } from "../../../shared/types/general.type";
import { FormUserContent } from "./FormUser";
import { IRol } from "ui-mf-users/src/shared/types/rol.types";

interface IErrorRP {
  data: Record<string, string[]>;
}

const formatError = (error: any): React.ReactElement => {
  const errorRP = error as IErrorRP;
  let array: any[] = [];
  Object.keys(errorRP.data).map((x) => {
    array = array.concat(errorRP.data[x]);
    return undefined;
  });
  return (
    <div>
      {array.map((x) => (
        <div key={x}>{x}</div>
      ))}
    </div>
  );
};

interface IUserForm {
  data?: IUser;
  form?: UseFormReturn<any, any, any>;
}

export const Index = (props: IFormProps): React.ReactElement => {
  const [dataForm, setDataForm] = useState<IUserForm>({data: {status: true}});
  const urlBase = buildMSUrl("/v1/users") ?? "";
  const urlRol = buildMSUrl("/v1/roles") ?? "";
  const urlQuery = `${urlBase}/${props.id}`;

  const { data, isLoading } = useQuery<IUser>(urlQuery, {
    queryKey: [`${urlQuery}-${props.id}`],
    enabled: props.id !== undefined,
  });
  const { data: dataRol, isLoading: isLoadingRol } = useQuery<IRol[]>(urlRol, {
    queryKey: [`${urlRol}`],
    enabled: true,
  });

  useEffect(() => {
    if (!isLoading && data) {
      setDataForm({ data: data });
    }
  }, [data, isLoading])

  const post = useMutation<IUser, unknown, IUser>(urlBase, {
    request: { method: "post" },
    mutation: {
      onSuccess: (e, _, context) => {
        props.onSaved?.();
        notification.success({ message: localizedOps.OperationCompleted });
      },
      onError: (e) => {
        if (e.status === 400) {
          // props.onSaved?.();
          notification.warning({ message: formatError(e.body) });
        } else {
          props.onSaved?.();
          notification.error({ message: localizedOps.OperationFailed });
        }
      },
    },
  });

  const update = useMutation<IUser, unknown, IUser>(`${urlBase}/${props.id}`, {
    request: { method: "put" },
    mutation: {
      onSuccess: (e, _, context) => {
        props.onSaved?.();
        notification.success({ message: localizedOps.OperationCompleted });
      },
      onError: (e) => {
        if (e.status === 400) {
          notification.warning({ message: formatError(e.body) });
        } else {
          notification.error({ message: localizedOps.OperationFailed });
        }
      },
    },
  });

  const onChangeForm = useCallback((form?: UseFormReturn<FieldValues, any, FieldValues>) => {
    setDataForm({ ...dataForm, form } as any);
  }, [dataForm]);

  const onSave = async (): Promise<void> => {
    const data = dataForm?.form?.getValues();
    const keys = Object.keys(data);
    // await test();
    const result = await dataForm?.form?.trigger(keys);
    if (!result) {
      notification.warning({ message: localizedUI.InvalidModel });
    } else {
      delete data.id;
      data.Userid = identityUtil.user?.username;
      if (props?.id) update.mutate(data);
      else post.mutate(data);
    }
  };

  return (
    <Modal
      open={true}
      width={600}
      title={<div className="!text-[--rp-bg] !text-[18px] font-bold">{props.id ? "Modificar Usuario" : "Crear Usuario"}</div>}
      footer={[]}
      onCancel={props.onCancel}
      centered
      maskClosable={false}
    >
      <Loading visible={isLoading || post.isPending || update.isPending}>
        <div className="mt-[30px]">
          <FormUserContent initialData={props.id && data? dataForm.data : undefined} onChangeForm={onChangeForm} comboData={dataRol} />
        </div>
        <div className="flex flex-row items-center justify-end">
          <Space>
            <Button
              className="bg-[--rp-bg] hover:bg-[--hover-rp-bg]"
              shape="round"
              type="primary"
              onClick={onSave}
            >
              {localizedUI.Save}
            </Button>
          </Space>
        </div>
      </Loading>
    </Modal>
  );
};
