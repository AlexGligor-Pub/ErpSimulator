using AutoMapper;
using Domain.Entities.UNS;
using Domain.Entities.DTO.Response;

namespace Infrastructure.Mappers
{
    public class UnsOrderProfile : Profile
    {
        public UnsOrderProfile() 
        {
            CreateMap<UnsOrder, OrderPriorityResponse>()
                 .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority));

            CreateMap<UnsOrder, OrderStatusResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
