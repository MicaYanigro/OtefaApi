using Otefa.Domain.Model.Entities;
using System.Collections.Generic;
using System.Dynamic;

namespace Otefa.Domain.Model.Services
{
    public interface ITeamService
    {
        Team FindTeamByName(string name);

        Team Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

        void Update(int teamID, string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

        IEnumerable<Team> GetAll();

        Team GetByID(int id);

        IEnumerable<ExpandoObject> GetTeamStadistics(int teamID);

        ExpandoObject GetHistoricalStadistics(int teamID);
    }
}