﻿using Newtonsoft.Json;

namespace CapitalSchoolApi.Models
{
    public class Workflow
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        public string? ApplicationId { get; set; }
        public string? StageType { get; set; }
        public List<VideoInterview>?  videoInterviews { get; set; }
    }

    public class VideoInterview
    {
        public string? Id { get; set; }
        public string? InterviewQuestion { get; set; }
        public string? AdditonalInfo { get; set; }
        public string? MaxDuration { get; set; }
        public string? Deadline { get; set; }
    }
}
