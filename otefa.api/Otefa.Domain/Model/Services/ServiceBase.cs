using Otefa.Infrastructure.IoC;

namespace Otefa.Domain.Model.Services
{
    public class ServiceBase
    {
        public ServiceBase()
        {
            Container.Current.Inject(this);
        }
    }
}