
using Otefa.Domain.Model.Entities;
using System;

namespace Otefa.Domain.Model.Factories
{
    public interface IPlayerFactory
    {

        Player Create(string name, string lastName, string dni, DateTime birthDate,
                             string email, string celNumber, string medicalInsurance);

    }
}