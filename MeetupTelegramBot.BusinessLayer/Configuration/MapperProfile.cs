using AutoMapper;
using meetup_telegram_bot.SignalR.Models;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.BusinessLayer.SignalR.Models;
using MeetupTelegramBot.DataAccess.Entities;

namespace MeetupTelegramBot.BusinessLayer.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Feedbacks
        CreateMap<FeedbackDTO, FeedbackEntity>().ReverseMap();
        CreateMap<FeedbackDTO, FeedbackModel>()
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.GeneralFeedback))
            .ForMember(dest => dest.Sent, opt => opt.MapFrom(src => src.Time));
        
        // Questions
        CreateMap<QuestionDTO, QuestionEntity>().ReverseMap();
        CreateMap<QuestionDTO, QuestionModel>()
            .ForMember(dest => dest.Asked, opt => opt.MapFrom(src => src.Time))
            .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Text));
        
        // Presentations
        CreateMap<PresentationDTO, PresentationEntity>();
        CreateMap<PresentationEntity, PresentationDTO>()
            .ForMember(p => p.SpeackerName, opt => opt.MapFrom(src => $"{src.Speacker.FirstName} {src.Speacker.LastName}"));
        CreateMap<PresentationForCreationDto, PresentationEntity>().ReverseMap();
        CreateMap<PresentationDTO, PresentationModel>();
        CreateMap<PresentationDTO, PresentationModel>();
        
        // Meetups
        CreateMap<MeetupEntity, MeetupDTO>();
        CreateMap<MeetupDTO, MeetupModel>();
    }
}