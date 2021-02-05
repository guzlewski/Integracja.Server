using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Integracja.Server.Core.Enums;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GamemodeService : IGamemodeService
    {
        private readonly IGamemodeRepository _gamemodeRepository;
        private readonly IMapper _mapper;

        public GamemodeService(IGamemodeRepository gamemodeRepository, IMapper mapper)
        {
            _gamemodeRepository = gamemodeRepository;
            _mapper = mapper;
        }

        public async Task<GamemodeGet> Get(int id, int userId)
        {
            var entity = await _gamemodeRepository.Get(id, userId)
              .Select(gm => new GamemodeGet
              {
                  Id = gm.Id,
                  Name = gm.Name,
                  TimeForFullQuiz = gm.TimeForFullQuiz,
                  TimeForOneQuestion = gm.TimeForOneQuestion.GetValueOrDefault(),
                  NumberOfLives = gm.NumberOfLives.GetValueOrDefault(),
                  IsPublic = gm.IsPublic,
                  OwnerId = gm.OwnerId,
                  OwnerUsername = gm.Owner.UserName
              })
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<GamemodeGetAll>> GetAll(int userId)
        {
            return await _gamemodeRepository.GetAll(userId)
                .Select(gm => new GamemodeGetAll
                {
                    Id = gm.Id,
                    Name = gm.Name,
                    TimeForFullQuiz = gm.TimeForFullQuiz,
                    TimeForOneQuestion = gm.TimeForOneQuestion.GetValueOrDefault(),
                    NumberOfLives = gm.NumberOfLives.GetValueOrDefault(),
                    IsPublic = gm.IsPublic,
                    GamesCount = gm.Games.Where(g => g.GameState != GameState.Deleted).Count(),
                    OwnerId = gm.OwnerId,
                    OwnerUsername = gm.Owner.UserName
                })
                .ToListAsync();
        }

        public async Task<int> Add(GamemodeAdd dto, int userId)
        {
            var gamemode = _mapper.Map<Gamemode>(dto);
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

        public async Task<int> Update(int id, GamemodeModify dto, int userId)
        {
            var gamemode = _mapper.Map<Gamemode>(dto);

            gamemode.Id = id;
            gamemode.OwnerId = userId;

            return await _gamemodeRepository.Update(gamemode);
        }
    }
}
