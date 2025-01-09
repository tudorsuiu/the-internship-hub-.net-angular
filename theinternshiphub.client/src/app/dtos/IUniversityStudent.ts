import { IInternship } from './IInternship';

export interface IUniversityStudent {
    id: string;
    studentFirstName: string;
    studentLastName: string;
    studentEmail: string;
    studentPhoneNumber: string;
    university: string;
    applicationId: string;
    internshipId: string;
    internship?: IInternship;
    applicationStatus: string;
    applicationCVFilePath: string;
    applicationUniversityDocsFilePath: string;
}
