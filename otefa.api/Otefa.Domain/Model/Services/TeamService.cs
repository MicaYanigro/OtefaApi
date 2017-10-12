using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Otefa.Domain.Model.Services
{
    public class TeamService : ServiceBase, ITeamService
    {

        [Injectable]
        public ITeamFactory TeamFactory { get; set; }

        [Injectable]
        public ITeamRepository TeamRepository { get; set; }

        [Injectable]
        public IPlayerRepository PlayerRepository { get; set; }

        [Injectable]
        public IMatchTeamRepository MatchTeamRepository { get; set; }



        public Team FindTeamByName(string name)
        {
            return TeamRepository.GetByName(name);

        }

        public async Task<Team> GetByID(int id)
        {
            return await TeamRepository.GetByIDAsync(id);

        }


        public async Task<Team> Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList)
        {
            {

                var Team = TeamFactory.Create(name, teamDelegate, shieldImage, teamImage, playersList);

                TeamRepository.Add(Team);
                await TeamRepository.Context.Commit();

                return Team;
            }
        }

        public async Task Update(int teamID, string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList)
        {
            var team = TeamRepository.GetById(teamID);

            var players = new List<Player>();
            foreach (var playerID in playersList)
            {
                var player = PlayerRepository.GetById(playerID);
                players.Add(player);
            }

            team.Update(name,
                        teamDelegate,
                        shieldImage,
                        teamImage,
                        players);

            TeamRepository.Update(team);
            await TeamRepository.Context.Commit();
        }

        public IEnumerable<Team> GetAll()
        {
            return TeamRepository.All();

        }

        public IEnumerable<ExpandoObject> GetTeamStadistics(int teamID)
        {

            return MatchTeamRepository.GetTeamStadistics(teamID);

        }

        public ExpandoObject GetHistoricalStadistics(int teamID)
        {

            return MatchTeamRepository.GetHistoricalStadistics(teamID);

        }

        public IEnumerable<Match> GetUpcomingMatches(int teamID)
        {

            return MatchTeamRepository.GetUpcomingMatches(teamID);

        }

        
    }
}