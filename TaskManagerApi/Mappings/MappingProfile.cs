using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Enums;
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

            CreateMap<Store, ItemTypeResponse>()
                .ForMember(destination => destination.ItemId,
                    member => member.MapFrom(field => field.StoreId))
                .ForMember(destination => destination.Description,
                    member => member.MapFrom(field => field.Name))
                .ForMember(destination => destination.Type,
                    member => member.MapFrom(field => ItemType.Store));

            CreateMap<Assignment, ItemTypeResponse>()
                .ForMember(destination => destination.ItemId,
                    member => member.MapFrom(field => field.AssignmentId))
                .ForMember(destination => destination.Description,
                    member => member.MapFrom(field => field.Description))
                .ForMember(destination => destination.Type,
                    member => member.MapFrom(field => ItemType.Assignment));

            CreateMap<Status, ItemTypeResponse>()
                .ForMember(destination => destination.ItemId,
                    member => member.MapFrom(field => field.StatusId))
                .ForMember(destination => destination.Description,
                    member => member.MapFrom(field => field.Description))
                .ForMember(destination => destination.Type,
                    member => member.MapFrom(field => ItemType.Status));
        }
    }
}