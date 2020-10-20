using System;
using System.ComponentModel;

namespace TaskManagerApp.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }

        [DisplayName("Fecha")]
        public DateTime Date { get; set; }

        public int AssignmentId { get; set; }

        [DisplayName("Tarea")]
        public string AssignmentDescription { get; set; }

        [DisplayName("StoreName")]
        public string StoreName { get; set; }

        public int StatusId { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription { get; set; }

        [DisplayName("Nota")]
        public string Note { get; set; }
    }
}