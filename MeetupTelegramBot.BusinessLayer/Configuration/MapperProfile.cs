using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.BusinessLayer.SignalR.Models;
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.BusinessLayer.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<FeedbackDTO, FeedbackEntity>().ReverseMap();
        CreateMap<QuestionDTO, QuestionEntity>().ReverseMap();
        CreateMap<PresentationDTO, PresentationEntity>();
        CreateMap<PresentationEntity, PresentationDTO>()
            .ForMember(p => p.SpeackerName, opt => opt.MapFrom(src => $"{src.Speacker.FirstName} {src.Speacker.LastName}"));
        CreateMap<PresentationForCreationDto, PresentationEntity>().ReverseMap();
        CreateMap<MeetupEntity, MeetupDTO>();
        CreateMap<MeetupDTO, MeetupModel>();
    }
}