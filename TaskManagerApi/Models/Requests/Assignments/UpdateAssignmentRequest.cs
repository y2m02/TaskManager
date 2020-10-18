using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Assignments
{
    public class UpdateAssignmentRequest : AssignmentRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? AssignmentId { get; set; }
    }
}