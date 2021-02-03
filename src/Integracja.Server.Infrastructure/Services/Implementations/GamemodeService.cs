using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public GamemodeService(IGamemodeRepository gamemodeRepository)
        {
            _gamemodeRepository = gamemodeRepository;
        }

        public async Task<GamemodeDto> Get(int id, int userId)
        {
            var entity = await _gamemodeRepository.Get(id, userId)
              .Select(gm => new GamemodeDto
              {
                  Id = gm.Id,
                  IsPublic = gm.IsPublic,
                  Name = gm.Name,
                  TimeForFullQuiz = gm.TimeForFullQuiz,
                  TimeForOneQuestion = gm.TimeForOneQuestion,
                  NumberOfLives = gm.NumberOfLives,
                  AuthorId = gm.AuthorId,
                  AuthorUsername = gm.Author.UserName,
                  GamesCount = gm.Games.Where(g => g.GameState != GameState.Deleted).Count()
              })
              .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return entity;
        }

        public async Task<IEnumerable<GamemodeDto>> GetAll(int userId)
        {
            var entities = await _gamemodeRepository.GetAll(userId)
                .Select(gm => new GamemodeDto
                {
                    Id = gm.Id,
                    IsPublic = gm.IsPublic,
                    Name = gm.Name,
                    TimeForFullQuiz = gm.TimeForFullQuiz,
                    TimeForOneQuestion = gm.TimeForOneQuestion,
                    NumberOfLives = gm.NumberOfLives,
                    AuthorId = gm.AuthorId,
                    AuthorUsername = gm.Author.UserName,
                    GamesCount = gm.Games.Where(g => g.GameState != GameState.Deleted).Count()
                })
                .ToListAsync();

            return entities;
        }

        public async Task<GamemodeDto> Add(GamemodeDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                dto.TimeForFullQuiz < 1 ||
                dto.TimeForOneQuestion.GetValueOrDefault() < 0 ||
                dto.NumberOfLives.GetValueOrDefault() < 0)
            {
                throw new BadRequestException();
            }

            var entity = await _gamemodeRepository.Add(new Gamemode
            {
                IsPublic = dto.IsPublic,
                AuthorId = userId,
                Name = dto.Name,
                TimeForFullQuiz = dto.TimeForFullQuiz,
                TimeForOneQuestion = dto.TimeForOneQuestion,
                NumberOfLives = dto.NumberOfLives
            });

            return new GamemodeDto
            {
                Id = entity.Id,
                IsPublic = entity.IsPublic,
                AuthorId = entity.AuthorId,
                Name = entity.Name,
                TimeForFullQuiz = entity.TimeForFullQuiz,
                TimeForOneQuestion = entity.TimeForOneQuestion,
                NumberOfLives = entity.NumberOfLives
            };
        }

        public async Task Delete(int id, int userId)
        {
            await _gamemodeRepository.Delete(new Gamemode
            {
                Id = id,
                AuthorId = userId
            });
        }

        public async Task Update(int id, GamemodeDto dto, int userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                dto.TimeForFullQuiz < 1 ||
                dto.TimeForOneQuestion.GetValueOrDefault() < 0 ||
                dto.NumberOfLives.GetValueOrDefault() < 0)
            {
                throw new BadRequestException();
            }

            var gamemode = new Gamemode
            {
                Id = id,
                IsPublic = dto.IsPublic,
                AuthorId = userId,
                Name = dto.Name,
                TimeForFullQuiz = dto.TimeForFullQuiz,
                TimeForOneQuestion = dto.TimeForOneQuestion,
                NumberOfLives = dto.NumberOfLives
            };

            await _gamemodeRepository.Update(gamemode);
        }
    }
}
