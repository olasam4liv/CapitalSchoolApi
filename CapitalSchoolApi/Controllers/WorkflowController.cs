using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CapitalSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService _workflowService;

        public WorkflowController(IWorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [Route("UpdateWorkFlow")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWorkFlow(WorkflowDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _workflowService.UpdateWorkFlow(payload);

            if (serviceResponse.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.BadRequest, serviceResponse);
            }

            if (serviceResponse.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, serviceResponse);
            }
            return StatusCode(statusCode: (int)HttpStatusCode.OK, serviceResponse);
        }
    }
}
