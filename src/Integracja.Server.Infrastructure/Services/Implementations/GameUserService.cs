using System;
using System.Threading.Tasks;
using Integracja.Server.Core.Repositories;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class GameUserService : IGameUserService
    {
        private readonly IGameUserRepository _gameUserRepository;

        public GameUserService(IGameUserRepository gameUserRepository)
        {
            _gameUserRepository = gameUserRepository;
        }

        public async Task Join(Guid gameGuid, int userId)
        {
            await _gameUserRepository.Join(gameGuid, userId);
        }

        public async Task Leave(Guid gameGuid, int userId)
        {
            await _gameUserRepository.Leave(gameGuid, userId);
        }
    }
}
