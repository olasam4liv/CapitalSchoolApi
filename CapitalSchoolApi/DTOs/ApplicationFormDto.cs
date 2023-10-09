using CapitalSchoolApi.Models;

namespace CapitalSchoolApi.DTOs
{
    public class ApplicationFormDto
    {
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
        public List<QuestionModelDto>? Questions { get; set; }
        public List<EducationDto>? Educations { get; set; }
        public List<ExperienceDto>? Experiences { get; set; }
        public List<AdditionalQuestionDto>? additionalQuestions { get; set; }
    }
    public class QuestionModelDto
    {       
        public string Type { get; set; }
        public string Question { get; set; }

    }
    public class EducationDto
    {        
        public string School { get; set; }
        public string Degree { get; set; }
        public string Location { get; set; }
        public string Course { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class ExperienceDto
    {        
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    public class AdditionalQuestionDto
    {
         
        public string Paragraph { get; set; }
        public string Dropdown { get; set; }
        public string Question { get; set; }
        public List<QuestionChoiceDto> Choice { get; set; }

    }

    public class QuestionChoiceDto
    {       
        public string Choice { get; set; }
    }
}
