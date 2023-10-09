using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Models;
using CapitalSchoolApi.Response;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Configuration;
using System.Net;

namespace CapitalSchoolApi.Services
{
    public class ApplicationService:IApplicationService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        public ApplicationService(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;


            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "ApplicationForms";
            var database = _cosmosClient.GetDatabase(databaseName);
            _container = database.GetContainer(containerName);
        }

        public async Task<ServiceResponse<dynamic>> CreateApplication(ApplicationFormDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            var questions = new List<QuestionModel>();
            var educations = new List<Education>();
            var experiences = new List<Experience>();
            var additionalQuestions = new List<AdditionalQuestion>();
            var choices = new List<QuestionChoice>(); 
            try
            {
                //Check if data exist
                var query = await _container.GetItemLinqQueryable<ApplicationForm>()
                                       .Where(u => u.ProgramId == payload.ProgramId)
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.FirstOrDefault();


                if (response != null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"Application already Created for the program Id {payload.ProgramId}";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    return serviceResponse;
                }


                foreach (var item in payload.Questions)
                {
                    var question = new QuestionModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        Question = item.Question,
                        Type = item.Type
                    };
                    questions.Add(question);
                }

                foreach (var item in payload.Educations)
                {

                    var education = new Education
                    {
                        Id = Guid.NewGuid().ToString(),
                        Course = item.Course,
                        Degree = item.Degree,
                        Location = item.Location,
                        School = item.School,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                    };
                    educations.Add(education);
                }

                foreach (var item in payload.Experiences)
                {
                    var experience = new Experience
                    {
                        Id = Guid.NewGuid().ToString(),
                        Company = item.Company,
                        Location = item.Location,
                        Title = item.Title,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                    };
                    experiences.Add(experience);
                }

                
                foreach (var item in payload.additionalQuestions)
                {

                    foreach (var item2 in item.Choice)
                    {

                        var choice = new QuestionChoice 
                        { 
                            Id = Guid.NewGuid().ToString(),
                            Choice = item2.Choice 
                        };

                        choices.Add(choice);
                    }

                    var additionalQuestion = new AdditionalQuestion
                    {
                        Id= Guid.NewGuid().ToString(),
                        Choice= choices,
                        Dropdown = item.Dropdown,
                        Paragraph = item.Paragraph,
                        Question = item.Question
                    };

                    additionalQuestions.Add(additionalQuestion);
                }

                
                //Create new Application
                var newApllicationForm = new ApplicationForm
                {
                    Id = Guid.NewGuid().ToString(),
                    ProgramId= payload.ProgramId,
                    CurrentResidence= payload.CurrentResidence,
                    Dob= payload.Dob,
                    Email= payload.Email,
                    FileDoc= payload.FileDoc,
                    FirstName= payload.FirstName,
                    Gender= payload.Gender,
                    IdNumber= payload.IdNumber,
                    LastName= payload.LastName,
                    Nationality= payload.Nationality,
                    Phone=payload.Phone,
                    Questions= questions,
                    Educations = educations,
                    Experiences= experiences,
                    additionalQuestions= additionalQuestions,     
                };

                var res = await _container.CreateItemAsync<ApplicationForm>(newApllicationForm);

                if (res.StatusCode == HttpStatusCode.Created)
                {
                    serviceResponse.Data = payload;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Program Succesfully Created";
                    serviceResponse.StatusCode = (int)HttpStatusCode.OK;

                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Program Not Created";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (CosmosException ex)
            {

                serviceResponse.Data = null;
                serviceResponse.Message = "Internal Server Error";
                serviceResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<dynamic>> GetAllApplications()
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {

                var query = await _container.GetItemLinqQueryable<ApplicationForm>()
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.ToList();


                if (response == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Record not found";
                    serviceResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    return serviceResponse;
                }

                serviceResponse.Data = response;
                serviceResponse.Success = true;
                serviceResponse.Message = "Record Succesfully fetched";
                serviceResponse.StatusCode = (int)HttpStatusCode.OK;

            }
            catch (CosmosException ex)
            {

                serviceResponse.Data = null;
                serviceResponse.Message = "Internal Server Error";
                serviceResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return serviceResponse;
        }
    }
}
