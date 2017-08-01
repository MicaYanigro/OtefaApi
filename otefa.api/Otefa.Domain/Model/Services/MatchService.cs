using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public class MatchService : ServiceBase, IMatchService
    {

        [Injectable]
        public IMatchFactory MatchFactory { get; set; }

        [Injectable]
        public IMatchRepository MatchRepository { get; set; }

        [Injectable]
        public IMatchTeamRepository MatchTeamRepository { get; set; }

        [Injectable]
        public IHeadquarterRepository HeadquarterRepository { get; set; }

        [Injectable]
        public IPlayerDetailsFactory PlayerDetailsFactory { get; set; }


        public Match Create(int headquarterID, DateTime date, IEnumerable<int> teamsID)
        {
            {
                var headquarter = HeadquarterRepository.GetById(headquarterID);
                var Match = MatchFactory.Create(headquarter, date, teamsID);

                MatchRepository.Add(Match);
                MatchRepository.Context.Commit();

                return Match;
            }
        }

        public void AddPlayerDetails(int matchTeamID, int playerID, int? goals, bool played, Card? card, string observation)
        {
            var matchTeam = MatchTeamRepository.GetById(matchTeamID);
            var playerDetails = PlayerDetailsFactory.Create(playerID, goals, played, card, observation);
            matchTeam.AddPlayerDetails(playerDetails);
        }


        public void Update(int matchID, int headquarterID, DateTime date)
        {
            var match = MatchRepository.GetById(matchID);

            var headquarter = HeadquarterRepository.GetById(headquarterID);
            
            match.Update(headquarter, date);

            MatchRepository.Update(match);
            MatchRepository.Context.Commit();
        }

        public void LoadResults(int matchTeamID, int goals, bool hasBonusPoint)
        {
            var matchTeam = MatchTeamRepository.GetById(matchTeamID);

            matchTeam.Update(goals, hasBonusPoint);

            MatchTeamRepository.Update(matchTeam);
            MatchTeamRepository.Context.Commit();
        }


        public IEnumerable<Match> GetAll()
        {
            return MatchRepository.All();

        }

    }
}