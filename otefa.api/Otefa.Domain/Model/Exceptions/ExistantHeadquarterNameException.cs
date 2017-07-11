using Otefa.Domain.Model.Exceptions;

namespace Otefa.Domain.Model.Exceptions
{
    public class ExistantHeadquarterNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Headquarter-Name-Already-Exists";
            }

        }
    }
}
