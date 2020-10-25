namespace TaskManagerApi.Models.Requests.Stores
{
    public class DeleteStoreRequest
    {
        public DeleteStoreRequest(int id)
        {
            StoreId = id;
        }

        public int StoreId { get; }
    }
}