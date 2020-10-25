using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Statuses
{
    public class UpdateStatusRequest : StatusRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? StatusId { get; set; }
    }
}