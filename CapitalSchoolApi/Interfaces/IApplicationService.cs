using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Response;

namespace CapitalSchoolApi.Interfaces
{
    public interface IApplicationService
    {
        Task<ServiceResponse<dynamic>> CreateApplication(ApplicationFormDto payload);
        Task<ServiceResponse<dynamic>> GetAllApplications();

    }
}
