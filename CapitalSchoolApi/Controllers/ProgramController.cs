using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CapitalSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        public ProgramController()
        {

        }


        [HttpGet("Hello")]
        public string Hello()
        {
            return "Hello, World!";
        }

    }
}
