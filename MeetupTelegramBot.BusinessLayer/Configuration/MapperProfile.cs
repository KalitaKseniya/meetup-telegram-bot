using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.BusinessLayer.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<FeedbackDTO, FeedbackEntity>().ReverseMap();
        CreateMap<QuestionDTO, QuestionEntity>().ReverseMap();
        CreateMap<PresentationDTO, PresentationEntity>().ReverseMap();
        CreateMap<PresentationForCreationDto, PresentationEntity>().ReverseMap();
    }
}