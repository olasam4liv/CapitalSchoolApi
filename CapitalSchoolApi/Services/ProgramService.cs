using Azure;
using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Models;
using CapitalSchoolApi.Response;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Net;

namespace CapitalSchoolApi.Services
{
    public class ProgramService: IProgramService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        public ProgramService(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            

            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "Programs";
            var database = _cosmosClient.GetDatabase(databaseName);           
            _container = database.GetContainer(containerName);
        }

        public async Task<ServiceResponse<dynamic>> Register(ProgramDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
                var query = await _container.GetItemLinqQueryable<ProgramModel>()
                                       .Where(u => u.Title == payload.Title)
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.FirstOrDefault();

                
                if(response != null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Program already Created";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    return serviceResponse;
                }

                //Create new Program
                var newProgram = new ProgramModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = payload.Title,
                    ApplicationClose = payload.ApplicationClose,
                    ApplicationCriteria = payload.ApplicationCriteria,
                    ApplicationOpen = payload.ApplicationOpen,
                    Benefit = payload.Benefit,
                    Description = payload.Description,
                    Duration = payload.Duration,
                    KeySkills = payload.KeySkills,
                    Location = payload.Location,
                    MaxNumofApplication = payload.MaxNumofApplication,
                    MinQualification = payload.MinQualification,
                    ProgramType = payload.ProgramType,
                    StartDate = payload.StartDate,
                    Summary = payload.Summary,
                    CreatedAt = DateTime.Now,                     
                };

                var res = await _container.CreateItemAsync<ProgramModel>(newProgram);

                if(res.StatusCode == HttpStatusCode.Created)
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
    }
}
