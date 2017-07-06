namespace Otefa.Domain.Model.Exceptions
{
    public class NullNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Null-Name";
            }

        }
    }
}