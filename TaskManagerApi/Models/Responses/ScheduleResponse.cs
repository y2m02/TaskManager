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

            var totalDays = 0;

            var date = Date.Date;

            while (date <= endDate)
            {
                if (IsWeekend(date))
                {
                    date = date.AddDays(1);
                    continue;
                }

                totalDays += 1;

                date = date.AddDays(1);
            }

            return totalDays;
        }

        private bool IsWeekend(DateTime date)
        {
            return
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}