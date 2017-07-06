using Otefa.Domain.Model.Exceptions;

namespace Otefa.Domain.Model.Exceptions
{
    public class ExistantTeamNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Team-Name-Already-Exists";
            }

        }
    }
}
