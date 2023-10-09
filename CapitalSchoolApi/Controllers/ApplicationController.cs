using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Response;
using CapitalSchoolApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CapitalSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        [Route("Register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateApplication(ApplicationFormDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _applicationService.CreateApplication(payload);

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

        [Route("GetAllApplications")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPrograms()
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _applicationService.GetAllApplications();

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
