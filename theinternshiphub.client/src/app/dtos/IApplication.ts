import { IInternship } from './IInternship';
import { IUser } from './IUser';

export interface IApplication {
    id: string;
    internshipId: string;
    internship: IInternship;
    studentId: string;
    student: IUser;
    appliedDate: Date;
    status: string;
    cvFilePath: string;
    isDeleted: boolean;
}
