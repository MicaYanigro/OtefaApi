using Otefa.Infrastructure.IoC;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Otefa.UI.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiControllerBase : ApiController
    {
        public ApiControllerBase()
        {
            Container.Current.Inject(this);
        }
    }
}