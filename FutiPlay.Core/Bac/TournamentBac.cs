using FutiPlay.Core.Interfaces.IRepository;
using FutiPlay.Core.Response;
using FutiPlay.Core.Interfaces.IBac;
using FutiPlay.Core.Models;
using System;
using xShared.Responses;
using xShared.Request;
using Microsoft.AspNetCore.Identity;
using FutiPlay.Core.Identity.Enums;

namespace FutiPlay.Core.Bac
{
	public class TournamentBac : ITournamentBac
	{
		private readonly ITournamentRepository _tournamentRepository;

        public TournamentBac(ITournamentRepository tournamentRepository)
		{
			_tournamentRepository = tournamentRepository;
		}

        /// <summary>
        /// Fetches tournament by request
        /// </summary>
        /// <returns>Response object</returns>
        public async Task<TournamentResponse> FetchTournamentByRequestAsync()
		{
			TournamentResponse tournaments = await _tournamentRepository.FetchTournamentByRequestAsync();

			return tournaments;

        }

        /// <summary>
        /// Insert Tournament by request
        /// </summary>
        /// <param name="request">Request containing the model to be inserted</param>
        /// <returns>Response of inserted data</returns>
        public async Task<ModelOperationResponse> InsertTournamentByRequestAsync(Tournament tournament)
		{
			ModelOperationRequest<Tournament> request = new(tournament);
			request.Model.SetCreateAtDateAndTime();

            ModelOperationResponse response = await _tournamentRepository.InsertTournamentByRequestAsync(request);

			return response;
		}
	}
}
