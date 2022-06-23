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
    private readonly IMapper _mapper;

    public PresentationService(IPresentationRepository presentationRepository, IMapper mapper)
    {
        _presentationRepository = presentationRepository;
        _mapper = mapper;
    }

    public async Task<PresentationDTO> CreateAsync(PresentationForCreationDto presentationDto)
    {
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

    public async Task<List<PresentationDTO>> GetDisplayedAsync()
    {
        var dbPresentations = await _presentationRepository.GetDisplayedAsync();
        return _mapper.Map<List<PresentationDTO>>(dbPresentations);
    }
    
    public async Task UpdateDisplayedAsync(List<PresentationForUpdateDto> presentationsToUpdate)
    {
        var ids = presentationsToUpdate.Select(p => p.Id).ToList();
        var notContainAll = _presentationRepository.NotContainsAll(ids);

        if (notContainAll)
        {
            throw new EntityNotFoundException("One or more ids are not found in the database.");
        }

        var presentationsIdsToUpdate = presentationsToUpdate.Select(x => x.Id).ToList();
        foreach (var id in presentationsIdsToUpdate)
        {
            var presentationExist = _presentationRepository.PresentationExist(id);

            if (!presentationExist)
            {
                throw new EntityNotFoundException(typeof(PresentationEntity).Name, id);
            }
        }

        //var oldPresentatnions = _presentationRepository.FindByCondition(p => p.IsDisplayed == true, true);
        //await oldPresentatnions.ForEachAsync(x => x.IsDisplayed = false);
        
        //var newPresentations = _presentationRepository.FindByCondition(p => presentationsIdsToUpdate.Contains(p.Id), true);
        //await newPresentations.ForEachAsync(x => x.IsDisplayed = true);

        await _presentationRepository.SaveChangesAsync();
    }
}