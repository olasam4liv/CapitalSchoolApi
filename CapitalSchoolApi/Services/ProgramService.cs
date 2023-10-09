using Azure;
using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Models;
using CapitalSchoolApi.Response;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Newtonsoft.Json;
using System.Net;

namespace CapitalSchoolApi.Services
{
    public class ProgramService: IProgramService
    {

        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _container;
        private readonly ILogger<ProgramService> _log;
        public ProgramService(CosmosClient cosmosClient, IConfiguration configuration, ILogger<ProgramService> log)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            _log = log;
            

            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "Programs";
            var database = _cosmosClient.GetDatabase(databaseName);           
            _container = database.GetContainer(containerName);
        }

        public async Task<ServiceResponse<dynamic>> GetAllPrograms()
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
              
                var query = await _container.GetItemLinqQueryable<ProgramModel>()
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

        public async Task<ServiceResponse<dynamic>> GetProgramById(string programId)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
                //Check if data exist
                var query = await _container.GetItemLinqQueryable<ProgramModel>()
                                       .Where(u => u.Id == programId.Trim())
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.FirstOrDefault();


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

        public async Task<ServiceResponse<dynamic>> Register(ProgramDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
                //Check if data exist
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
                _log.LogInformation("New Program Request", JsonConvert.SerializeObject(newProgram));
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
                _log.LogError("Error occured while updating Program", JsonConvert.SerializeObject(ex));
            }

           return serviceResponse;
        }

        public async Task<ServiceResponse<dynamic>> UpdateProgram(UpdateProgramDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            try
            {
                //Check if data exist
                var query = await _container.GetItemLinqQueryable<ProgramModel>()
                                       .Where(u => u.Id == payload.Id)
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.FirstOrDefault();


                if (response == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Record not found";
                    serviceResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    return serviceResponse;
                }


                //Update Program

                response.Title = payload.Title;
                response.ApplicationClose = payload.ApplicationClose;
                response.ApplicationCriteria = payload.ApplicationCriteria;
                response.ApplicationOpen = payload.ApplicationOpen;
                response.Benefit = payload.Benefit;
                response.Description = payload.Description;
                response.Duration = payload.Duration;
                response.KeySkills = payload.KeySkills;
                response.Location = payload.Location;
                response.MaxNumofApplication = payload.MaxNumofApplication;
                response.MinQualification = payload.MinQualification;
                response.ProgramType = payload.ProgramType;
                response.StartDate = payload.StartDate;
                response.Summary = payload.Summary;
                response.UpdatedAt = DateTime.Now;

                _log.LogInformation("Update Program Request", JsonConvert.SerializeObject(payload));
                var res = await _container.ReplaceItemAsync<ProgramModel>(response, payload.Id);

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    serviceResponse.Data = payload;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Record Succesfully Updated";
                    serviceResponse.StatusCode = (int)HttpStatusCode.OK;

                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Record Not Updated";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (CosmosException ex)
            {

                serviceResponse.Data = null;
                serviceResponse.Message = "Internal Server Error";
                serviceResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                _log.LogError("Error occured while updating Program", JsonConvert.SerializeObject(ex));
            }

            return serviceResponse;
        }
    }
}
