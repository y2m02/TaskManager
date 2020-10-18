using System;
using AutoMapper;

namespace TaskManagerApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Category, GetCategoryResponse>();
            //CreateMap<CreateCategoryRequest, Category>();
            //CreateMap<UpdateCategoryRequest, Category>();
            //CreateMap<DeleteCategoryRequest, Category>()
            //    .ForMember(destination => destination.DeletedOn,
            //        member => member.MapFrom(field => DateTime.Now));
        }
    }
}