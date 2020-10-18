using System;

namespace TaskManagerApi.Models.Responses
{
    public class ScheduleResponse
    {
        public int ScheduleId { get; set; }

        public DateTime Date { get; set; }

        public int AssignmentId { get; set; }

        public string AssignmentDescription { get; set; }

        public int StatusId { get; set; }

        public string StatusDescription { get; set; }

        public string Note { get; set; }
    }
}