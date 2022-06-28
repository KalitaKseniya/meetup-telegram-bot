using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.BusinessLayer.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(QuestionDTO entity)
    {
        return _questionRepository.CreateAsync(_mapper.Map<QuestionEntity>(entity));
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