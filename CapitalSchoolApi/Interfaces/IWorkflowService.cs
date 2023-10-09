using CapitalSchoolApi.DTOs;
using CapitalSchoolApi.Response;

namespace CapitalSchoolApi.Interfaces
{
    public interface IWorkflowService
    {
        Task<ServiceResponse<dynamic>> UpdateWorkFlow(WorkflowDto payload);
    }
}
