using System;
using System.ComponentModel;

namespace TaskManagerApp.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }

        [DisplayName("Iniciada en")]
        public DateTime Date { get; set; }

        [DisplayName("Finalizada en")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Días transcurridos")]
        public double TotalDays { get; set; }

        public int AssignmentId { get; set; }

        [DisplayName("Tarea")]
        public string AssignmentDescription { get; set; }

        [DisplayName("Tienda")]
        public string StoreName { get; set; }

        public int StatusId { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription { get; set; }

        [DisplayName("Nota")]
        public string Note { get; set; }
    }
}