namespace TaskManagerApi.Models.Responses
{
    public class StatusResponse
    {
        public int StatusId { get; set; }

        public string Description { get; set; }

        public bool Used { get; set; }
    }
}