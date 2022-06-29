using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.BusinessLayer.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMeetupPresentationRepository _meetupPresentationsRepository;
    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, 
        IMapper mapper,
        IMeetupPresentationRepository meetupPresentationRepository)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _meetupPresentationsRepository = meetupPresentationRepository;
    }

    public async Task CreateAsync(QuestionDTO questionDto)
    {
        var presentationId = questionDto.PresentationId;
        var meetupId = questionDto.MeetupId;
        var meetupPresentation = _meetupPresentationsRepository.GetByMeetupAndPresentation(meetupId, presentationId); 
        
        //ToDo: should we throw exception or add meetup presentation entity?
        if (meetupPresentation == null)
        {
            meetupPresentation = new MeetupPresentationEntity
            {
                MeetupId = meetupId,
                PresentationId = presentationId
            };
            await _meetupPresentationsRepository.CreateAsync(meetupPresentation);
            //throw new EntityNotFoundException($"MeetupPresentation for meetup {meetupId} and presentation {presentationId}");
        }
        var questionEntity = _mapper.Map<QuestionEntity>(questionDto);
        questionEntity.MeetupPresentationId = meetupPresentation.Id;
        
        await _questionRepository.CreateAsync(questionEntity);
    }

    public async Task<List<QuestionDTO>> GetAllAsync()
    {
        return _mapper.Map<List<QuestionDTO>>(await _questionRepository.GetAllAsync());
    }

    public async Task<List<QuestionDTO>> GetByMeetupPresentationIdAsync(Guid meetupId, Guid presentationId)
    {
        var dbQuestions = await _questionRepository.GetByMeetupPresentationAsync(meetupId, presentationId);
        return _mapper.Map<List<QuestionDTO>>(dbQuestions);
    }
}