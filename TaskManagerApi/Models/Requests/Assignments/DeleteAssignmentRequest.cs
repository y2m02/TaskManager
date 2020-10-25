namespace TaskManagerApi.Models.Requests.Assignments
{
    public class DeleteAssignmentRequest
    {
        public DeleteAssignmentRequest(int id)
        {
            AssignmentId = id;
        }

        public int AssignmentId { get; }
    }
}