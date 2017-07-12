using Otefa.Domain.Model.Exceptions;

namespace Otefa.Domain.Model.Exceptions
{
    public class ExistantTournamentNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Tournament-Name-Already-Exists";
            }

        }
    }
}
