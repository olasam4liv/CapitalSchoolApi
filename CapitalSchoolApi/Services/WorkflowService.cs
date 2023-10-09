using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Models;
using CapitalSchoolApi.Response;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace CapitalSchoolApi.Services
{
    public class WorkflowService:IWorkflowService
    {
        
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;
        private readonly Container _appContainer;
        private readonly Container _workflowContainer;
        private readonly ILogger<WorkflowService> _log;
        public WorkflowService(CosmosClient cosmosClient, IConfiguration configuration, ILogger<WorkflowService> log)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;


            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "Workflows";
            var appContainerName = "ApplicationForms";
            var database = _cosmosClient.GetDatabase(databaseName);
            _appContainer = database.GetContainer(appContainerName);
            _workflowContainer = database.GetContainer(containerName);
            _log = log;
        }
        public async Task<ServiceResponse<dynamic>> UpdateWorkFlow(WorkflowDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();
            var workflows = new List<Workflow>();
            var videoInterview = new List<VideoInterview>();
            try
            {
                //Check if data exist
                var query = await _appContainer.GetItemLinqQueryable<ApplicationForm>()
                                       .Where(u => u.Id == payload.ApplicationId)
                                       .ToFeedIterator().ReadNextAsync();
                var response = query.FirstOrDefault();


                if (response == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Success = true;
                    serviceResponse.Message = $"No Record Found";
                    serviceResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    return serviceResponse;
                }


                foreach (var item in payload.videoInterviews)
                {
                    var videointerview = new VideoInterview
                    {
                        Id = Guid.NewGuid().ToString(),
                        AdditonalInfo = item.AdditonalInfo,
                        Deadline = item.Deadline,
                        InterviewQuestion = item.InterviewQuestion,
                        MaxDuration = item.MaxDuration



                    };
                    videoInterview.Add(videointerview);
                }

                var workflow = new Workflow
                {
                    Id = Guid.NewGuid().ToString(),
                    ApplicationId = payload.ApplicationId,                   
                    StageType = payload.StageType,
                    videoInterviews = videoInterview
                };

              

                _log.LogInformation("Update Workflow Request", JsonConvert.SerializeObject(workflow));
                var res = await _workflowContainer.CreateItemAsync<Workflow>(workflow);

                if (res.StatusCode == HttpStatusCode.Created)
                {
                    serviceResponse.Data = payload;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Wrokflow Succesfully Updated";
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
                _log.LogError("Error occured while updating workflow", JsonConvert.SerializeObject(ex));
            }

            return serviceResponse;

        }
    }
}
