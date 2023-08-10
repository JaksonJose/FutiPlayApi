using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Models;
using FutiPlay.Core.Response;

namespace FutiPlay.Core.Bac
{
    public class PlayerBac : IPlayerBac
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerBac(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// Fetches all players
        /// </summary>
        /// <returns>Response object with players list
        /// and messages</returns>
        public async Task<PlayerResponse> FetchAllPlayersAsync()
        {
            PlayerResponse response = await _playerRepository.FetchAllPlayersAsync();

            return response;
        }
    }
}
