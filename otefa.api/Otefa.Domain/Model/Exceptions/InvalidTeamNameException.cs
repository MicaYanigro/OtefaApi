using Otefa.Domain.Model.Exceptions;

namespace Otefa.Domain.Model.Exceptions
{
    public class InvalidTeamNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Invalid-Name";
            }

        }
    }
}
