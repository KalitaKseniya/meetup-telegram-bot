using AutoMapper;
using MeetupTelegramBot.BusinessLayer.Interfaces;
using MeetupTelegramBot.BusinessLayer.Models.DTO;
using MeetupTelegramBot.DataAccess.Interfaces;

namespace MeetupTelegramBot.BusinessLayer.Services
{
    public class MeetupService : IMeetupService
    {
        private readonly IMeetupRepository _meetupRepository;
        private readonly IMapper _mapper;

        public MeetupService(IMeetupRepository meetupRepository, IMapper mapper)
        {
            _meetupRepository = meetupRepository;
            _mapper = mapper;
        }

        public async Task<List<MeetupDTO>> GetAllAsync()
        {
            var dbMeetups = await _meetupRepository.GetAllAsync();
            return _mapper.Map<List<MeetupDTO>>(dbMeetups);
        }
    }
}
