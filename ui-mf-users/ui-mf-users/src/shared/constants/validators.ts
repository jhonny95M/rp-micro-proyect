import { localizedValidations } from "./strings";

export const required = (
  msg?: string,
): {
  required: {
    message: string;
    value: true;
  };
} => {
  return {
    required: {
      message: msg ?? localizedValidations.Required,
      value: true,
    },
  };
};

export const maxLength = (
  max: number,
  msg?: string,
): {
  maxLength: {
    message: string;
    value: number;
  };
} => {
  return {
    maxLength: {
      message: msg ?? `${localizedValidations.MaxLength} ${max?.toString()}`,
      value: max,
    },
  };
};
export const email = (
  msg?: string,
): {
  pattern: {
    message: string;
    value: RegExp;
  };
} => {
  return {
    pattern: {
      message: msg ?? localizedValidations.Email,
      value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
    },
  };
};
