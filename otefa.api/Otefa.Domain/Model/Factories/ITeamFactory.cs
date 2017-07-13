﻿
using Otefa.Domain.Model.Entities;
using System.Collections.Generic;

namespace Otefa.Domain.Model.Factories
{
    public interface ITeamFactory
    {

        Team Create(string name, string teamDelegate, string shieldImage, string teamImage, IEnumerable<int> playersList);

    }
}