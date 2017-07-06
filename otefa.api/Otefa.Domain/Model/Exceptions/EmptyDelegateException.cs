namespace Otefa.Domain.Model.Exceptions
{
    public class EmptyDelegateException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Empty-Delegate";
            }

        }
    }
}