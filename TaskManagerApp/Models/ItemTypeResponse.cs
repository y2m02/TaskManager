using TaskManagerApp.Models.Enums;

namespace TaskManagerApp.Models
{
    public class ItemTypeResponse
    {
        public int ItemId { get; set; }

        public string Description { get; set; }

        public ItemType Type { get; set; }
    }
}
