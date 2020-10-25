using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Assignments
{
    public class AssignmentRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        [StringLength(500, ErrorMessage = "El Campo {0} no puede exceder de {1} caracteres.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public int? StoreId { get; set; }
    }
}