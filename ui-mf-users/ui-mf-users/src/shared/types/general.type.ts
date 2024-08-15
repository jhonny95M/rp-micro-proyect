export interface IGeneralEntity {
  isValid?: boolean;
  modification_username?: string;
  modification_datetime?: Date;
}

export interface IFormProps {
  requestId?: number;
  id?: string;
  data?: any;
  visible?: boolean;
  httpCall?: boolean;
  flowCall?: boolean;
  infoCall?: boolean;
  onCancel?: () => void;
  onSaved?: (id?: string | null) => void;
}


export interface IConfirmOperationProps {
  id?: string;
  data?: any;
  visible?: boolean;
  onCancel?: () => void;
  onCompleted?: () => void;
}
