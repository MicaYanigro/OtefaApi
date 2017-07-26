using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Services
{
    public interface ITeamService
    {
        Team FindTeamByName(string name);

        Team Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

        void Update(int teamID, string name, string teamDelegate, string shieldImage, string teamImage);

        IEnumerable<Team> GetAll();
        Team GetByID(int id);
    }
}