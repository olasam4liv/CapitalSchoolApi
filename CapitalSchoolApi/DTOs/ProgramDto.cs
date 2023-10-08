namespace CapitalSchoolApi.DTOs
{
    public class ProgramDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string KeySkills { get; set; }
        public string Benefit { get; set; }
        public string ApplicationCriteria { get; set; }
        public string ProgramType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ApplicationClose { get; set; }
        public DateTime ApplicationOpen { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string MinQualification { get; set; }
        public string MaxNumofApplication { get; set; }
    }
}
