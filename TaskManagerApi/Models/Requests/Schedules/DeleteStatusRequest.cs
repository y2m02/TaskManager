namespace TaskManagerApi.Models.Requests.Schedules
{
    public class DeleteScheduleRequest
    {
        public DeleteScheduleRequest(int id)
        {
            ScheduleId = id;
        }

        public int ScheduleId { get; }
    }
}