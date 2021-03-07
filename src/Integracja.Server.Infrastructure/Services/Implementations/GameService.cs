using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public GameService(IGameRepository gameRepository, IMapper mapper, IConfigurationProvider configuration)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<DetailGameDto> Get(int id, int userId)
        {
            var detailGameDto = await _gameRepository.Get(id)
                .Where(g => g.OwnerId == userId && g.GameState != GameState.Deleted)
                .ProjectTo<DetailGameDto>(_configuration)
                .FirstOrDefaultAsync();

            if (detailGameDto == null)
            {
                throw new NotFoundException();
            }

            return detailGameDto;
        }

        public async Task<IEnumerable<GameDto>> GetAll(int userId)
        {
            return await _gameRepository.GetAll()
                .Where(g => g.OwnerId == userId && g.GameState != GameState.Deleted)
                .ProjectTo<GameDto>(_configuration)
                .ToListAsync();
        }

        public async Task<int> Add(CreateGameDto createGameDto, int userId)
        {
            var game = _mapper.Map<Game>(createGameDto);
            game.OwnerId = userId;
            game.Guid = Guid.NewGuid();

            return await _gameRepository.Add(game, createGameDto.QuestionsCount.Value, createGameDto.RandomizeQuestionOrder.Value);
        }

        public async Task Delete(int id, int userId)
        {
            await _gameRepository.Delete(new Game
            {
                Id = id,
                OwnerId = userId
            });
        }

        public async Task<int> Update(int id, EditGameDto dto, int userId)
        {
            var game = _mapper.Map<Game>(dto);
            game.Id = id;
            game.OwnerId = userId;

            return await _gameRepository.Update(game);
        }
    }
}
