using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Factories;
using Otefa.Domain.Model.Repositories;
using Otefa.Infrastructure.IoC;
using System;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public class TeamService : ServiceBase, ITeamService
    {

        [Injectable]
        public ITeamFactory TeamFactory { get; set; }

        [Injectable]
        public ITeamRepository TeamRepository { get; set; }

        public Team FindTeamByName(string name)
        {
            return TeamRepository.GetByName(name);

        }

        public Team GetByID(int id)
        {
            return TeamRepository.GetById(id);

        }


        public Team Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList)
        {
            {

                var Team = TeamFactory.Create(name, teamDelegate, shieldImage, teamImage, playersList);

                TeamRepository.Add(Team);
                TeamRepository.Context.Commit();

                return Team;
            }
        }

        public void Update(int teamID, string name, string teamDelegate, string shieldImage, string teamImage)
        {
            var team = TeamRepository.GetById(teamID);

            team.Update(name,
                        teamDelegate,
                        shieldImage,
                        teamImage);

            TeamRepository.Update(team);
            TeamRepository.Context.Commit();
        }

        public IEnumerable<Team> GetAll()
        {
            return TeamRepository.All();

        }

    }
}