/* eslint-env node */
import { App, notification } from "antd";
import React, { useEffect, useRef } from "react";
import { localizedOps, localizedUI } from "../../../../shared/constants/strings"; 
import { useMutation, identityUtil } from "web-utilities";
import { buildMSUrl } from "../../../../shared/constants/urls";
import { IConfirmOperationProps } from "../../../../shared/types/general.type";
import { IUser } from "../../../../shared/types/user.types";

export const ConfirmEnable = ({
  id,
  data,
  onCompleted,
  onCancel,
}: IConfirmOperationProps): React.ReactElement => {
  const { modal } = App.useApp();
  const isOpened = useRef(false);
  const urlBase = buildMSUrl(`/v1/users/status/${id}`) ?? "";
  const opStatus = useMutation<IUser, unknown, IUser>(urlBase, {
    request: { method: "put" },
    mutation: {
      onSuccess: (e, _, context) => {
        onCompleted?.();
        notification.success({ message: localizedOps.OperationCompleted });
      },
      onError: (e) => {
        notification.error({ message: localizedOps.OperationFailed });
      },
    },
  });

  useEffect(() => {
    if (!isOpened.current) {
      isOpened.current = true;
      modal.confirm({
        title: (
          <>
            <div className="!text-[--rp-bg] font-[700] text-[16px] w-[100%]">
              {data.status ? localizedOps.OperationEnable : localizedOps.OperationDisable}
            </div>
          </>
        ),
        icon: null,
        content: localizedOps.ConfirmAction,
        okButtonProps: {
          shape: "round",
          type: "primary",
          className: "bg-[--rp-bg] hover:bg-[--hover-rp-bg]",
        },
        okText: localizedUI.Continue,
        centered: true,
        cancelText: localizedUI.Cancel,
        cancelButtonProps: { shape: "round", type: "default" },
        onOk: () => {
          opStatus.mutate({
            status: data.status ?? false,
            userId: identityUtil.user?.username,
          });
          onCompleted?.();
        },
        onCancel: () => {
          onCancel?.();
        },
      });
    }
  }, [isOpened.current]);

  return <></>;
};
