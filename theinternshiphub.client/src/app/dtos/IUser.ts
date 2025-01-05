import { ICompany } from './ICompany';
import { Role } from './IUserRegister';

export interface IUser {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    company: ICompany;
    role: Role | null;
    city: string;
    isDeleted: boolean;
}
