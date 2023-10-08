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
                return StatusCode(statusCode: (int)HttpStatusCode.BadRequest, serviceResponse);
            }
            if (serviceResponse.StatusCode == (int)HttpStatusCode.PaymentRequired)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.PaymentRequired, serviceResponse);
            }
            if (serviceResponse.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                return StatusCode(statusCode: (int)HttpStatusCode.InternalServerError, serviceResponse);
            }
            return StatusCode(statusCode: (int)HttpStatusCode.OK, serviceResponse);


        }

    }
}
