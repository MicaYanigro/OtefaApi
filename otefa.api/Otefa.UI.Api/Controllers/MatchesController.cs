using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/matches")]
    public class MatchesController : ApiControllerBase
    {

        public MatchesController()
        {
        }

        [Injectable]
        public IMatchService MatchService { get; set; }

        public MatchesController(IMatchService MatchService)
        {
            this.MatchService = MatchService;
        }
        
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(MatchViewModel MatchViewModel)
        {
            try
            {
                var Match = MatchService.Create(MatchViewModel.Headquarter, MatchViewModel.Date, MatchViewModel.Teams);

                return Request.CreateResponse(HttpStatusCode.Created, Match.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public IEnumerable<Match> Get()
        {
            return MatchService.GetAll();
        }

        [HttpPut]
        [Route("{MatchID}")]
        public HttpResponseMessage Put([FromUri] int MatchID, [FromBody]PutMatchViewModel PutMatchViewModel)
        {
            try
            {
                MatchService.Update(MatchID, PutMatchViewModel.Headquarter, PutMatchViewModel.Date, PutMatchViewModel.Teams);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}