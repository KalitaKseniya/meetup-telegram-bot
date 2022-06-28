using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.BusinessLayer.Models.DTO.Request;
using MeetupTelegramBot.DataAccess.Entities;
using MeetupTelegramBot.DataAccess.Exceptions;
using MeetupTelegramBot.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupTelegramBot.BusinessLayer.Services;

public class PresentationService : IPresentationService
{
    private readonly IPresentationRepository _presentationRepository;
    private readonly ISpeackerRepository _speackerRepository;
    private readonly IMapper _mapper;

    public PresentationService(IPresentationRepository presentationRepository,
        ISpeackerRepository speackerRepository,
        IMapper mapper)
    {
        _presentationRepository = presentationRepository;
        _speackerRepository = speackerRepository;
        _mapper = mapper;
    }

    public async Task<PresentationDTO> CreateAsync(PresentationForCreationDto presentationDto)
    {

        var speacker = _speackerRepository.GetById(presentationDto.SpeackerId);
        if (speacker == null)
        {
            throw new EntityNotFoundException("Speacker", presentationDto.SpeackerId);
        }
        var entity = _mapper.Map<PresentationEntity>(presentationDto);

        _presentationRepository.Create(entity);
        await _presentationRepository.SaveChangesAsync();

        return _mapper.Map<PresentationDTO>(entity);
    }

    public async Task<List<PresentationDTO>> GetAllAsync()
    {
        var dbPresentations = await _presentationRepository.GetAllAsync();
        return _mapper.Map<List<PresentationDTO>>(dbPresentations);
    }
}