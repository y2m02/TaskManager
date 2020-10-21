using System.ComponentModel;

namespace TaskManagerApp.Models.ViewModels
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        public int StoreId { get; set; }

        [DisplayName("Tienda")]
        public string StoreName { get; set; }

        public int StatusId { get; set; }

        [DisplayName("Estado")]
        public string StatusDescription { get; set; }

        public bool Used { get; set; }
    }
}