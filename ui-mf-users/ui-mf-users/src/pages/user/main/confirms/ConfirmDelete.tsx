/* eslint-env node */
/* eslint-disable @typescript-eslint/restrict-template-expressions */

import { App, notification } from "antd";
import React, { useEffect, useRef } from "react";
import { localizedOps, localizedUI } from "../../../../shared/constants/strings"; 
import { buildMSUrl } from "../../../../shared/constants/urls";
import { useMutation } from "web-utilities";
import { IConfirmOperationProps } from "../../../../shared/types/general.type";

export const ConfirmDelete = ({
  id,
  data,
  onCompleted,
  onCancel,
}: IConfirmOperationProps): React.ReactElement => {
  const { modal } = App.useApp();
  const isOpened = useRef(false);
  const urlBase = buildMSUrl(`/v1/users`) ?? new URL("");

  const opDel = useMutation<unknown>(`${urlBase}/${id}`, {
    request: { method: "delete" },
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
              {localizedOps.OperationDelete}
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
          opDel.mutate();
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
