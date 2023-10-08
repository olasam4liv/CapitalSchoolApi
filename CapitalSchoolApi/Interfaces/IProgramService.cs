using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Response;

namespace CapitalSchoolApi.Interfaces
{
    public interface IProgramService
    {
        Task<ServiceResponse<dynamic>> Register(ProgramDto payload);
        Task<ServiceResponse<dynamic>> UpdateProgram(UpdateProgramDto payload);
        Task<ServiceResponse<dynamic>> GetProgramById(string programId);
        Task<ServiceResponse<dynamic>> GetAllPrograms();
    }
}
