namespace Otefa.Domain.Model.Exceptions
{
    public class EmptyNameException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Empty-Name";
            }

        }
    }
}