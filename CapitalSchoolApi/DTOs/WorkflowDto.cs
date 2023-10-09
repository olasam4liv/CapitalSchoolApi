using CapitalSchoolApi.Models;

namespace CapitalSchoolApi.DTOs
{
    public class WorkflowDto
    {
        public string? ApplicationId { get; set; }
        public string? StageType { get; set; }
        public List<VideoInterviewDto>? videoInterviews { get; set; }
    }
    public class VideoInterviewDto
    {       
        public string? InterviewQuestion { get; set; }
        public string? AdditonalInfo { get; set; }
        public string? MaxDuration { get; set; }
        public string? Deadline { get; set; }
    }

}
