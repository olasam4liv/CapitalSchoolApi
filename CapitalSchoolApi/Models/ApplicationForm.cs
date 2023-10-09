using Newtonsoft.Json;

namespace CapitalSchoolApi.Models
{
    public class ApplicationForm
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public string FileDoc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public List<QuestionModel>? Questions { get; set; }
        public List<Education>? Educations { get; set; }
        public List<Experience>? Experiences { get; set; }
        public List<AdditionalQuestion>? additionalQuestions { get; set; }
        public List<Workflow>? Workflows { get; set; }
    }
    public class QuestionModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Question { get; set; }
        
    }

    public class Education
    {
        public string Id { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string Location { get; set; }
        public string Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class Experience
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class AdditionalQuestion
    {
        public string Id { get; set; }
        public string Paragraph { get; set; }
        public string Dropdown { get; set; }
        public string Question { get; set; }
        public List<QuestionChoice> Choice { get; set; }        

    }
    public class QuestionChoice
    {
        public string Id { get; set; }        
        public string Choice { get; set; }
    }
}
