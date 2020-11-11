using System;

namespace TaskManagerApi.Models.Responses
{
    public class ScheduleResponse
    {
        public int ScheduleId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? EndDate { get; set; }

        public double TotalDays => GetTotalDays();

        public int AssignmentId { get; set; }

        public string AssignmentDescription { get; set; }

        public string StoreName { get; set; }

        public int StatusId { get; set; }

        public string StatusDescription { get; set; }

        public string Note { get; set; }

        private double GetTotalDays()
        {
            var endDate = EndDate == null
                ? DateTime.Now.Date
                : EndDate.GetValueOrDefault().Date;

            var totalDays = (endDate - Date).TotalDays;

            return totalDays < 1
                ? 1
                : totalDays;
        }
    }
}