using System.ComponentModel;

namespace TaskManagerApp.Models.ViewModels
{
    public class CreateAssignmentRequest
    {
        public int AssignmentId { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Tienda")]
        public int StoreId { get; set; }

        [DisplayName("Estado")]
        public int StatusId { get; set; }
    }
}