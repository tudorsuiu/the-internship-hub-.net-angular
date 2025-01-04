import { ICompany } from './ICompany';
import { IUser } from './IUser';

export interface IInternshipAdd {
    title: string;
    description: string;
    company: ICompany | undefined;
    recruiter: IUser | null;
    startDate: Date | null;
    endDate: Date | null;
    positionsAvailable: number | null;
    compensation: number | null;
    isDeleted: boolean;
}
