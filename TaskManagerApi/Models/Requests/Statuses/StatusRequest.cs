using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Statuses
{
    public class StatusRequest
    {
        [Required(ErrorMessage = "Campo {0} requerido.")]
        [StringLength(100, ErrorMessage = "El Campo {0} no puede exceder de {1} caracteres.")]
        public string Description { get; set; }
    }
}