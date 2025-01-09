import { IAdditionalInformationDTO } from './IAdditionalInformationDTO';
import { IEducationDTO } from './IEducationDTO';
import { ILanguageDTO } from './ILanguageDTO';
import { IPersonalInformationDTO } from './IPersonalInformationDTO';
import { IProfessionalExperienceDTO } from './IProfessionalExperienceDTO';
import { IProjectDTO } from './IProjectDTO';

export interface ICandidateProfileDTO {
    personalInformation: IPersonalInformationDTO;
    education: IEducationDTO[];
    professionalExperience: IProfessionalExperienceDTO[];
    technicalSkills: string[];
    certifications: string[];
    projects: IProjectDTO[];
    languages: ILanguageDTO[];
    additionalInformation: IAdditionalInformationDTO;
}
