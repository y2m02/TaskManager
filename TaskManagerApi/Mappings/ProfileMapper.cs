using AutoMapper;
using System;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Enums;
using TaskManagerApi.Models.Requests.Assignments;
using TaskManagerApi.Models.Requests.Schedules;
using TaskManagerApi.Models.Requests.Statuses;
using TaskManagerApi.Models.Requests.Stores;
using TaskManagerApi.Models.Responses;

namespace TaskManagerApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Store, StoreResponse>()
                .ForMember(destination => destination.Used,
                    member => member.MapFrom(field => field.Assignments.Count > 0));
            CreateMap<StoreRequest, Store>();
            CreateMap<UpdateStoreRequest, Store>();
            CreateMap<DeleteStoreRequest, Store>();

            CreateMap<Status, StatusResponse>()
                .ForMember(destination => destination.Used,
                    member => member.MapFrom(field => field.Schedules.Count > 0));
            CreateMap<StatusRequest, Status>();
            CreateMap<UpdateStatusRequest, Status>();
            CreateMap<DeleteStatusRequest, Status>();

            CreateMap<Assignment, AssignmentResponse>()
                .ForMember(destination => destination.StoreName,
                    member => member.MapFrom(field => field.Store.Name))
                .ForMember(destination => destination.StatusDescription,
                    member => member.MapFrom(field =>
                        field.Schedule == null ? "Pendiente" : field.Schedule.Status.Description))
                .ForMember(destination => destination.Used,
                    member => member.MapFrom(field => field.Schedule != null));
            CreateMap<AssignmentRequest, Assignment>();
            CreateMap<UpdateAssignmentRequest, Assignment>();
            CreateMap<DeleteAssignmentRequest, Assignment>();

            CreateMap<Schedule, ScheduleResponse>()
                .ForMember(destination => destination.AssignmentDescription,
                    member => member.MapFrom(field => field.Assignment.Description))
                .ForMember(destination => destination.StoreName,
                    member => member.MapFrom(field => field.Assignment.Store.Name))
                .ForMember(destination => destination.StatusDescription,
                    member => member.MapFrom(field => field.Status.Description));
            CreateMap<ScheduleRequest, Schedule>()
                .ForMember(destination => destination.Date,
                    member => member.MapFrom(field => Convert.ToDateTime(field.Date.ToString("MMM/dd/yyyy"))))
                .ForMember(destination => destination.EndDate,
                    member => member.MapFrom(field => field.StatusId == 4 ? (DateTime?)DateTime.Now.Date : null));
            CreateMap<UpdateScheduleRequest, Schedule>()
                .ForMember(destination => destination.Date,
                    member => member.MapFrom(field => Convert.ToDateTime(field.Date.ToString("MMM/dd/yyyy"))))
                .ForMember(destination => destination.EndDate,
                    member => member.MapFrom(field => field.StatusId == 4 ? (DateTime?)DateTime.Now.Date : null));
            CreateMap<DeleteScheduleRequest, Schedule>();

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
                    member => member.MapFrom(field => $"{field.Store.Name.ToUpper()} - {field.Description}"))
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