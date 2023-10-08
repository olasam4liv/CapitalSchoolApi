using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Interfaces;
using CapitalSchoolApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CapitalSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }


        [Route("Register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(ProgramDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _programService.Register(payload);
            
            if (serviceResponse.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.BadRequest, serviceResponse);            }
            
            if (serviceResponse.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, serviceResponse);
            }
            return StatusCode(statusCode: (int)HttpStatusCode.OK, serviceResponse);


        }
        [Route("UpdateProgram")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProgram(UpdateProgramDto payload)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _programService.UpdateProgram(payload);

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

        [Route("GetProgramById/{programId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProgramById([Required] string programId)
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _programService.GetProgramById(programId);

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

        [Route("GetAllPrograms")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPrograms()
        {
            var serviceResponse = new ServiceResponse<dynamic>();

            serviceResponse = await _programService.GetAllPrograms();

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
