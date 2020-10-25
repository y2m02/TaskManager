using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Schedules
{
    public class UpdateScheduleRequest : ScheduleRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? ScheduleId { get; set; }
    }
}