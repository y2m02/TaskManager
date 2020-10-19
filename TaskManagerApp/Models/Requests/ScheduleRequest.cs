using System;
using System.ComponentModel;

namespace TaskManagerApp.Models.Requests
{
    public class ScheduleRequest
    {
        public int ScheduleId { get; set; }

        [DisplayName("Fecha")]
        public DateTime Date { get; set; }

        [DisplayName("Tarea")]
        public int AssignmentId { get; set; }

        [DisplayName("Estado")]
        public int StatusId { get; set; }

        [DisplayName("Nota")]
        public string Note { get; set; }
    }
}