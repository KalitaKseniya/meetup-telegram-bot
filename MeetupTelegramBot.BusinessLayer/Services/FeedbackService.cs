using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Exceptions;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.BusinessLayer.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IMeetupRepository _meetupRepository;
    private readonly IMapper _mapper;

    public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper, IMeetupRepository meetupRepository)
    {
        _feedbackRepository = feedbackRepository;
        _mapper = mapper;
        _meetupRepository = meetupRepository;
    }

    public  Task CreateAsync(FeedbackDTO entity)
    {
        //Some logic 

        var feedback = _mapper.Map<FeedbackEntity>(entity);
        return _feedbackRepository.CreateAsync(feedback);
    }

    public async Task<List<FeedbackDTO>> GetAllAsync()
    {
        return _mapper.Map<List<FeedbackDTO>>(await _feedbackRepository.GetAllAsync());
    }

    public async Task<FeedbackDTO> GetByIdAsync(Guid id)
    {
        return _mapper.Map<FeedbackDTO>(await _feedbackRepository.GetByIdAsync(id));
    }

    public async Task<List<FeedbackDTO>> GetByMeetupIdAsync(Guid meetupId)
    {
        if (!_meetupRepository.Exists(meetupId))
        {
            throw new EntityNotFoundException("Meetup", meetupId);
        }

        return _mapper.Map<List<FeedbackDTO>>(await _feedbackRepository.GetByMeetupIdAsync(meetupId));
    }
}