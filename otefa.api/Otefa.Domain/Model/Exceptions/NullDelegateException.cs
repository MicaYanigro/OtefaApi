namespace Otefa.Domain.Model.Exceptions
{
    public class NullDelegateException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Null-Delegate";
            }

        }
    }
}