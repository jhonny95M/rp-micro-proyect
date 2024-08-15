/* eslint-disable prettier/prettier */
/* eslint-disable @typescript-eslint/promise-function-async */
import React, { useCallback, useMemo, useState } from "react";
import { Button, Tooltip } from "antd";
// import { useQuery } from '@tanstack/react-query';
import { useQuery } from "web-utilities";
import {
  DeleteOutlined,
  DeploymentUnitOutlined,
  EditOutlined,
  PlusOutlined,
} from "@ant-design/icons";
import {
  Header,
  Filter,
  Container,
  TableRP,
  type IPaginationTable
} from "web-ui-design";
import {
  FILTER,
  type IPaginationResponse,
  type IPaginationRequest,
} from "../../../shared/constants/paginationConstants";
import { buildMSUrl } from "../../../shared/constants/urls";
import { IConfirmOperationProps, IFormProps, IUser } from "../../../shared/types";

import { ConfirmDelete, ConfirmEnable } from "./confirms";
import { columns, fieldsFilter } from "./constants";
import { Index as FormUser } from "./Form";

import "../styles.css";

export interface IPaginationFlowRequest extends IPaginationRequest {
  type: string;
  status: boolean | null | undefined;
}

function transformPagination<T>(
  response: IPaginationResponse<T> | undefined,
  isLoading: boolean,
): IPaginationTable<T> {
  const { data, paging } = response ?? ({} as IPaginationResponse<T>);
  return {
    items: data,
    loading: isLoading,
    page: paging?.currentIndex,
    size: paging?.pageSize,
    total: paging?.totalResults,
  };
}
export const UserMain = (): React.ReactElement => {
  const [confirmDelete, setConfirmDelete] = useState<IConfirmOperationProps>({ visible: false });
  const [confirmEnable, setConfirmEnable] = useState<IConfirmOperationProps>({ visible: false });
  const [formConnector, setFormConnector] = useState<IFormProps>({ visible: false });

  const [filters, setFilters] = useState<IPaginationFlowRequest>(FILTER as IPaginationFlowRequest);

  const filteredFilters = Object.fromEntries(
    Object.entries(filters).filter(([_, value]) => value !== null)
  );

  const url = buildMSUrl("/v1/users") ?? new URL("");
  url.search = new URLSearchParams(filteredFilters as any).toString();

  const { data, isLoading } = useQuery<IPaginationResponse<IUser>>(url ? url.toString() : "", {
    queryKey: [url.toString()]
  }); 

  const paginationTable = useMemo(() => {
    return transformPagination(data, isLoading);
  }, [data, isLoading]);

  const onChangePagination = (pag: any): void => {
    setFilters({ ...filters, pageCurrent: pag.current, pageSize: pag.pageSize });
  };

  const rowActions = [
    {
      title: "Editar",
      icon: <EditOutlined />,
      action: (row: any) => {
        onEdit(row);
      },
      visible: (row: any) => row.id,
    },
    {
      title: "Eliminar",
      icon: <DeleteOutlined />,
      action: (row: any) => {
        onDelete(row);
      },
      visible: (row: any) => row.id,
    }
  ];

  const onEdit = (item: IUser): void => {
    setFormConnector({ visible: true, id: item.id?.toString() ?? "", data: item });
  };

  const onChangeFilterForm = useCallback(
    (values: any): void => {
      const filteredValues: Record<string, any> = {};
      Object.keys(values).forEach((x: string) => {
        if (values[x] !== undefined) {    
          filteredValues[x] = values[x];
        }
      });
      setFilters({ ...filters, ...filteredValues });
    },
    [filters],
  );

  const onEnable = (item: any, status: boolean): void => {
    setConfirmEnable({
      visible: true,
      id: item.id,
      data: { status: status ? 1 : 0, type: item.type },
    });
  };
  const onEnabled = (): void => {
    setConfirmEnable({ visible: false });
    onChangeFilterForm({ requestId: Date.now() });
  };

  const onDelete = (item: any): void => {
    setConfirmDelete({ visible: true, id: item.id, data: { type: item.type } });
  };

  const onDeleted = (): void => {
    setConfirmDelete({ visible: false });
    onChangeFilterForm({ requestId: Date.now() });
  };

  const onCancelEnable = (): void => {
    setConfirmEnable({ visible: false });
  };
  const onCancelDelete = (): void => {
    setConfirmDelete({ visible: false });
  };

  const onSaved = (): void => {
    setFormConnector({ visible: false });
    onChangeFilterForm({ requestId: Date.now() });
  };

  const onCancel = (): void => {
    setFormConnector({ visible: false });
  };

  return (
    <Container>
      <Header title="Users" icon={<DeploymentUnitOutlined />} />
      <Filter
        onChangeForm={onChangeFilterForm}
        fields={fieldsFilter}
        tools={[
          <Tooltip title={"Crear Usuario"} key="abc">
            <Button
              className="bg-[--rp-bg] hover:bg-[--hover-rp-bg]"
              shape="circle"
              type="primary"
              onClick={() => {
                setFormConnector({ visible: true, id: undefined });
              }}
              icon={<PlusOutlined></PlusOutlined>}
            />
          </Tooltip>,
        ]}
      />
      <TableRP
        onChange={onChangePagination}
        columns={columns(onEnable)}
        pagination={paginationTable}
        actions={rowActions}
        key={"id"}

      />
      {formConnector.visible && (
        <FormUser
          id={formConnector.id}
          data={formConnector.data}
          onCancel={onCancel}
          onSaved={onSaved}
          requestId={Date.now()}
        />
      )}
      {confirmDelete.visible && (
        <ConfirmDelete
          id={confirmDelete.id}
          data={confirmDelete.data}
          onCancel={onCancelDelete}
          onCompleted={onDeleted}
        />
      )}
      {confirmEnable.visible && (
        <ConfirmEnable
          id={confirmEnable.id}
          data={confirmEnable.data}
          onCancel={onCancelEnable}
          onCompleted={onEnabled}
        />
      )}
    </Container>
  );
};
