export interface IUser {
    id?: number;
    username?: string;
    email?: string;
    password?: string;
    dateOfbirth?: Date;
    registrationDate?: Date;
    roleId?: number;
    status?: boolean;
  }