using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Response;

namespace CapitalSchoolApi.Interfaces
{
    public interface IProgramService
    {
        Task<ServiceResponse<dynamic>> Register(ProgramDto payload);
    }
}
