using System;

namespace Otefa.Domain.Model.Exceptions
{
    public class ExceptionBase : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Exception";
            }

        }
    }
}