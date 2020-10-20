using System.ComponentModel;

namespace TaskManagerApp.Models.Requests
{
    public class AssignmentRequest
    {
        public int AssignmentId { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Tienda")]
        public int StoreId { get; set; }
    }
}