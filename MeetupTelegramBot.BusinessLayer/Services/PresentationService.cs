using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.BusinessLayer.Services;

public class PresentationService : IPresentationService
{
    private readonly IPresentationRepository _presentationRepository;
    private readonly IMapper _mapper;

    public PresentationService(IPresentationRepository presentationRepository, IMapper mapper)
    {
        _presentationRepository = presentationRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(PresentationDTO entity)
    {
        return _presentationRepository.CreateAsync(_mapper.Map<PresentationEntity>(entity));
    }

    public async Task<List<PresentationDTO>> GetAllAsync()
    {
        return _mapper.Map<List<PresentationDTO>>( await _presentationRepository.GetAllAsync());
    }

    public async Task<List<PresentationDTO>> GetDisplayedAsync()
    {
        return _mapper.Map<List<PresentationDTO>>(await _presentationRepository.GetDisplayedAsync());
    }
}