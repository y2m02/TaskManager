namespace TaskManagerApi.Models.Requests.Statuses
{
    public class DeleteStatusRequest
    {
        public DeleteStatusRequest(int id)
        {
            StatusId = id;
        }

        public int StatusId { get; }
    }
}