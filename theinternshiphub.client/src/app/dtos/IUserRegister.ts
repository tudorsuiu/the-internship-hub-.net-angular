export enum Role {
    Student = 'Student',
    Recruiter = 'Recruiter',
    University = 'University',
}

export interface IUserRegister {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;
    role: Role | null;
    city: string;
    companyId: string;
}
