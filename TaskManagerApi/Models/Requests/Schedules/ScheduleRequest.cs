using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Schedules
{
    public class ScheduleRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? AssignmentId { get; set; }

        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? StatusId { get; set; }

        [StringLength(1000, ErrorMessage = "El Campo {0} no puede exceder de {1} caracteres.")]
        public string Note { get; set; }
    }
}