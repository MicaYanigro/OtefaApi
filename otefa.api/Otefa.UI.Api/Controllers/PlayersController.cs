using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Player;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/players")]
    public class PlayersController : ApiControllerBase
    {

        public PlayersController()
        {
        }

        [Injectable]
        public IPlayerService Playerservice { get; set; }

        public PlayersController(IPlayerService Playerservice)
        {
            this.Playerservice = Playerservice;
        }


        [Route("Dni/{dni}")]
        public Player GetByDni(string dni)
        {
            return Playerservice.FindPlayerByDni(dni);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(PlayerViewModel playerViewModel)
        {
            try
            {
                var player = await Playerservice.Create(playerViewModel.Name, playerViewModel.Lastname, playerViewModel.Dni,
                                 playerViewModel.BirthDate, playerViewModel.Email, playerViewModel.CelNumber, playerViewModel.MedicalInsurance);

                return Request.CreateResponse(HttpStatusCode.Created, player.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public IEnumerable<Player> Get()
        {
            return Playerservice.GetAll();
        }

        [HttpPut]
        [Route("{playerID}")]
        public HttpResponseMessage Put([FromUri] int playerID, [FromBody]PutPlayerViewModel PutPlayerViewModel)
        {
            try
            {
                Playerservice.Update(playerID, PutPlayerViewModel.Name, PutPlayerViewModel.Lastname, PutPlayerViewModel.Dni,
                            PutPlayerViewModel.BirthDate, PutPlayerViewModel.Email, PutPlayerViewModel.CelNumber, PutPlayerViewModel.MedicalInsurance);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}