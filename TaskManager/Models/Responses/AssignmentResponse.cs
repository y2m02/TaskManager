namespace TaskManager.Models.Responses
{
    public class AssignmentResponse
    {
        public int AssignmentId { get; set; }

        public string Description { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int StatusId { get; set; }

        public string StatusDescription { get; set; }
    }
}