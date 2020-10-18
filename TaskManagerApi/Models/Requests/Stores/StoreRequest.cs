using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Stores
{
    public class StoreRequest
    {
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public string Name { get; set; }
    }
}