using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models.Requests.Stores
{
    public class UpdateStoreRequest : StoreRequest
    {
        [Required(ErrorMessage = "Campo {0} es requerido.")]
        public int? StoreId { get; set; }
    }
}