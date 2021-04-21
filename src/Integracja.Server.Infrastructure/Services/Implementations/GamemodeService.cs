using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
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

        public async Task<T> Get<T>(int id, int userId, bool skipUserVerification = false)
        {
            var dto = await _gamemodeRepository.Get(id)
                .Where(gm => (gm.IsPublic || gm.OwnerId == userId || skipUserVerification) && !gm.IsDeleted)
                .ProjectTo<T>(_configuration)
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int userId, bool skipUserVerification = false)
        {
            return await _gamemodeRepository.GetAll()
                .Where(gm => (gm.IsPublic || gm.OwnerId == userId || skipUserVerification) && !gm.IsDeleted)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOwned<T>(int userId)
        {
            return await _gamemodeRepository.GetAll()
                .Where(gm => gm.OwnerId == userId &&
                    !gm.IsDeleted)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<int> Add(CreateGamemodeDto createGamemodeDto, int userId)
        {
            var gamemode = _mapper.Map<Gamemode>(createGamemodeDto);
            gamemode.OwnerId = userId;

            return await _gamemodeRepository.Add(gamemode);
        }

        public async Task Delete(int id, int userId, bool skipUserVerification = false)
        {
            await _gamemodeRepository.Delete(new Gamemode
            {
                Id = id,
                OwnerId = userId
            }, skipUserVerification);
        }

        public async Task<int> Update(int id, EditGamemodeDto editGamemodeDto, int userId, bool skipUserVerification = false)
        {
            var gamemode = _mapper.Map<Gamemode>(editGamemodeDto);
            gamemode.Id = id;
            gamemode.OwnerId = userId;

            return await _gamemodeRepository.Update(gamemode, skipUserVerification);
        }
    }
}
