using CapitalSchoolApi.Models;

namespace CapitalSchoolApi.DTOs
{
    public class UpdateApplicationFormDto
    {
        public string Id { get; set; }
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
    }

    
}
