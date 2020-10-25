using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models.ViewModels
{
    public class StatusViewModel
    {
        public int StatusId { get; set; }
        
        [DisplayName("Descripción")]
        [Required(ErrorMessage = "Campo {0} requerido.")]
        public string Description { get; set; }

        public bool Used { get; set; }
    }
}