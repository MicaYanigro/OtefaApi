namespace Otefa.Domain.Model.Exceptions
{
    public class ExistantPlayerDniException : ExceptionBase
    {

        public override string Message
        {
            get
            {
                return "Player-Dni-Already-Exists";
            }

        }
    }
}