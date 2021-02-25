using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GamemodeService : IGamemodeService
    {
        private readonly IGamemodeRepository _gamemodeRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GamemodeService(IGamemodeRepository gamemodeRepository, IMapper mapper, IConfigurationProvider configuration)
        {
            _gamemodeRepository = gamemodeRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<DetailGamemodeDto> Get(int id, int userId)
        {
            var entity = await _gamemodeRepository.Get(id, userId)
              .ProjectTo<DetailGamemodeDto>(_configuration)
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<GamemodeDto>> GetAll(int userId)
        {
            return await _gamemodeRepository.GetAll(userId)
                .ProjectTo<GamemodeDto>(_configuration)
                .ToListAsync();
        }

        public async Task<int> Add(CreateGamemodeDto createGamemodeDto, int userId)
        {
            var gamemode = _mapper.Map<Gamemode>(createGamemodeDto);
            gamemode.OwnerId = userId;

            return await _gamemodeRepository.Add(gamemode);
        }

        public async Task Delete(int id, int userId)
        {
            await _gamemodeRepository.Delete(new Gamemode
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, EditGamemodeDto editGamemodeDto, int userId)
        {
            var gamemode = _mapper.Map<Gamemode>(editGamemodeDto);
            gamemode.Id = id;
            gamemode.OwnerId = userId;

            return await _gamemodeRepository.Update(gamemode);
        }
    }
}
