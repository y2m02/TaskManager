using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Requests.Assignments;
using TaskManagerApi.Models.Requests.Schedules;
using TaskManagerApi.Models.Requests.Statuses;
using TaskManagerApi.Models.Requests.Stores;
using TaskManagerApi.Models.Responses;

namespace TaskManagerApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreResponse>();
            CreateMap<StoreRequest, Store>();
            CreateMap<UpdateStoreRequest, Store>();

            CreateMap<Status, StatusResponse>();
            CreateMap<StatusRequest, Status>();
            CreateMap<UpdateStatusRequest, Status>();

            CreateMap<Assignment, AssignmentResponse>()
                .ForMember(destination => destination.StoreName,
                    member => member.MapFrom(field => field.Store.Name))
                .ForMember(destination => destination.StatusDescription,
                    member => member.MapFrom(field =>
                        field.Schedule == null ? "Pendiente" : field.Schedule.Status.Description));
            CreateMap<Assignment, AssignmentForDropDownListResponse>();
            CreateMap<AssignmentRequest, Assignment>();
            CreateMap<UpdateAssignmentRequest, Assignment>();

            CreateMap<Schedule, ScheduleResponse>()
                .ForMember(destination => destination.AssignmentDescription,
                    member => member.MapFrom(field => field.Assignment.Description))
                .ForMember(destination => destination.StatusDescription,
                    member => member.MapFrom(field => field.Status.Description));
            CreateMap<ScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
        }
    }
}