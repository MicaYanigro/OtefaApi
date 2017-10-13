﻿using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

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

        [Injectable]
        public IPlayerRepository PlayerRepository { get; set; }

        [Injectable]
        public ITournamentRepository TournamentRepository { get; set; }

        [Injectable]
        public IGroupRepository GroupRepository { get; set; }


        public async Task<Match> Create(int tournamentID, int groupID, int headquarterID, DateTime date, int round, IEnumerable<int> teamsID)
        {
            {
                var headquarter = HeadquarterRepository.GetById(headquarterID);
                var Match = MatchFactory.Create(tournamentID, groupID, headquarter, date, round, teamsID);

                MatchRepository.Add(Match);
                await MatchRepository.Context.Commit();

                return Match;
            }
        }

        public void AddPlayerDetails(int matchTeamID, int playerID, int? goals, bool played, Card? card, string observation)
        {
            var matchTeam = MatchTeamRepository.GetById(matchTeamID);
            var playerDetails = PlayerDetailsFactory.Create(matchTeam, playerID, goals, played, card, observation);
            matchTeam.AddPlayerDetails(playerDetails);
        }


        public async Task Update(int matchID, int headquarterID, DateTime date)
        {
            var match = MatchRepository.GetById(matchID);

            var headquarter = HeadquarterRepository.GetById(headquarterID);

            match.Update(headquarter, date);

            MatchRepository.Update(match);
            await MatchRepository.Context.Commit();
        }
        

        public void LoadResults(int matchID, int matchTeamID, int goals, int againstGoals, bool hasBonusPoint, int figureID, IEnumerable<ExpandoObject> playersDetails)
        {
            var match = MatchRepository.GetById(matchID);
            var matchTeam = MatchTeamRepository.GetById(matchTeamID); 
            var figure = PlayerRepository.GetById(figureID);
            var playerDetailsList = new List<PlayerDetails>();

            foreach (dynamic playerDetail in playersDetails)
            {
                var playerDetailEntity = PlayerDetailsFactory.Create(matchTeam, playerDetail.PlayerID, playerDetail.Goals, playerDetail.Played, (Card)playerDetail.Card, playerDetail.Observation);
                playerDetailsList.Add(playerDetailEntity);
            }

            match.UpdateMatchTeam(matchTeamID, goals, againstGoals, hasBonusPoint, figure, playerDetailsList);
            match.CalculateFinalPoints();

            MatchRepository.Update(match);
            MatchRepository.Context.CommitNoAsync();
        }


        public IEnumerable<Match> GetAll()
        {
            return MatchRepository.All();

        }

        public async Task<Match> GetById(int matchId)
        {
            return await MatchRepository.GetByIDAsync(matchId);

        }

        public object GetByTournamentId(int tournamentId)
        {
            var tournament = TournamentRepository.GetById(tournamentId);
            var groups = tournament.GetGroups();
            var matchesList = new List<Match>();
            dynamic objectList = new ExpandoObject();
            var groupList = new List<ExpandoObject>();

            foreach (var group in groups)
            {
                var matches = GroupRepository.GetMatches(group);

                dynamic item = new ExpandoObject();

                item.Group = group.Name;
                item.Matches = matches;

                groupList.Add(item);
            }

            objectList.Tournament = tournament.GetId();
            objectList.Groups = groupList;

            return objectList;
        }


    }
}