import { ICompany } from './ICompany';
import { IUser } from './IUser';

export interface IInternship {
    id: string;
    title: string;
    description: string;
    company: ICompany;
    recruiter: IUser;
    startDate: Date;
    endDate: Date;
    positionsAvailable: number;
    compensation: number;
    isDeleted: boolean;
    deadline: Date;
    domain: string;
}
