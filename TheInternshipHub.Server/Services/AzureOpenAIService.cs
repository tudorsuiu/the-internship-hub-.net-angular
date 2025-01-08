using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using TheInternshipHub.Server.Domain.Interfaces;

namespace TheInternshipHub.Server.Services;

public class AzureOpenAIService : IAzureOpenAIService
{
    private readonly AzureOpenAIClient _azureOpenAIClient;
    private readonly string _rulesPrompt = "Acesta este un model AI care va analiza un CV din domeniul IT și va returna un rezumat în format JSON. CV-ul conține informații despre educație, experiență profesională, competențe tehnice, proiecte relevante și alte secțiuni. În calitate de recruiter IT, modelul trebuie să identifice informațiile cheie, să le extragă și să le organizeze într-un rezumat concis, structurat în format JSON. Asigură-te că fiecare secțiune este bine identificată și descrisă pe scurt. Formatul de ieșire trebuie să fie JSON și să includă următoarele câmpuri:\r\n\r\nPersonal Information - Numele, locația și contactul (dacă sunt disponibile).\r\nEducation - Instituțiile de învățământ și diplomele obținute.\r\nProfessional Experience - Companiile la care a lucrat și rolurile ocupate, cu o descriere scurtă a responsabilităților și realizărilor.\r\nTechnical Skills - Abilități tehnice și limbaje de programare (de exemplu, Python, Java, etc.).\r\nCertifications - Certificări relevante în domeniul IT.\r\nProjects - Proiecte relevante la care a lucrat, descrierea acestora și tehnologiile utilizate.\r\nLanguages - Limbi vorbite și nivelul de cunoaștere.\r\nAdditional Information - Orice alte informații relevante pentru recrutare, cum ar fi interesele sau voluntariatul.\r\nIeșirea trebuie să fie organizată în format JSON ca mai jos:\r\n{\r\n  \"personalInformation\": {\r\n    \"name\": \"Numele candidatului\",\r\n    \"location\": \"Locația candidatului\",\r\n    \"contact\": \"Email/Telefon\"\r\n  },\r\n  \"education\": [\r\n    {\r\n      \"institution\": \"Universitatea X\",\r\n      \"degree\": \"Licență/Master în Informatică\",\r\n      \"graduated\": \"current/yes\"\r\n    }\r\n  ],\r\n  \"professionalExperience\": [\r\n    {\r\n      \"company\": \"Compania Y\",\r\n      \"role\": \"Dezvoltator Software\",\r\n      \"duration\": \"2020 - prezent\",\r\n      \"responsibilities\": \"Descrierea responsabilităților și realizărilor principale.\"\r\n    }\r\n  ],\r\n  \"technicalSkills\": [\r\n    \"Python\", \r\n    \"Java\", \r\n    \"SQL\", \r\n    \"React\"\r\n  ],\r\n  \"certifications\": [\r\n    \"Certificare AWS\",\r\n    \"Certificare Scrum Master\"\r\n  ],\r\n  \"projects\": [\r\n    {\r\n      \"projectName\": \"Aplicație Web pentru Managementul Proiectelor\",\r\n      \"description\": \"O aplicație web de gestionare a proiectelor dezvoltate în Python și React.\",\r\n      \"technologiesUsed\": [\"Python\", \"React\", \"Django\"]\r\n    }\r\n  ],\r\n  \"languages\": [\r\n    {\r\n      \"name\": \"Română\",\r\n      \"level\": \"Nativ\"\r\n    },\r\n    {\r\n      \"language\": \"Engleză\",\r\n      \"level\": \"Avansat\"\r\n    }\r\n  ],\r\n  \"additionalInformation\": {\r\n    \"interests\": \"Programare, IoT, AI\",\r\n    \"volunteer\": \"Mentor pentru copii în domeniul IT\"\r\n  }\r\n}\r\nModelul trebuie să fie capabil să proceseze un CV și să-l analizeze corect în funcție de structura menționată, oferind un rezumat detaliat în format JSON. Dacă unele secțiuni nu sunt disponibile în CV, acestea pot fi lăsate ca null. Do not wrap the json codes in JSON markers.\"\r\n\r\nDetaliile din CV sunt urmatoarele:";

    public AzureOpenAIService(IConfiguration configuration)
    {
        _azureOpenAIClient = new AzureOpenAIClient(new Uri(configuration["AzureOpenAI:Endpoint"]), new ApiKeyCredential(configuration["AzureOpenAI:ApiKey"]));
    }

    public async Task<string> GetOpenAIResponse(string prompt)
    {
        try
        {
            var chatClient = _azureOpenAIClient.GetChatClient("gpt-4o-mini");

            var response = await chatClient.CompleteChatAsync(new UserChatMessage(_rulesPrompt + prompt));
            var responseString = response.Value.Content[0].Text;

            return responseString;
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}

