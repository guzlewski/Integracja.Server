using System;
using System.Threading.Tasks;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Services.Interfaces;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameUserService : IGameUserService
    {
        private readonly IGameUserRepository _gameUserRepository;

        public GameUserService(IGameUserRepository gameUserRepository)
        {
            _gameUserRepository = gameUserRepository;
        }

        public async Task<int> Join(Guid gameGuid, int userId)
        {
            return await _gameUserRepository.Join(gameGuid, userId);
        }

        public async Task Leave(int gameId, int userId)
        {
            await _gameUserRepository.Leave(gameId, userId);
        }
    }
}
