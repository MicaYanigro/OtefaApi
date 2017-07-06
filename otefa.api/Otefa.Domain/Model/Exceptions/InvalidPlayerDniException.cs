namespace Otefa.Domain.Model.Exceptions
{
    public class InvalidPlayerDniException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Invalid-Player-Dni";
            }

        }
    }
}