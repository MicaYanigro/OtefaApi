using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Otefa.UI.Api.Controllers
{
    [RoutePrefix("v1/tournaments")]
    public class TournamentsController : ApiControllerBase
    {

        public TournamentsController()
        {
        }

        [Injectable]
        public ITournamentService Tournamentservice { get; set; }

        public TournamentsController(ITournamentService Tournamentservice)
        {
            this.Tournamentservice = Tournamentservice;
        }

        [HttpGet]
        [Route("{id}")]
        public Tournament GetByID(int id)
        {
            return Tournamentservice.GetByID(id);
        }

        [HttpGet]
        [Route("Get/{name}")]
        public Tournament GetByName(string name)
        {
            return Tournamentservice.FindTournamentByName(name);
        }

        [HttpGet]
        [Route("{tournamentID}/matches")]
        public IEnumerable<Match> GetMatches(int tournamentID)
        {
            return Tournamentservice.GetAllMatches(tournamentID);
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post(TournamentViewModel TournamentViewModel)
        {
            try
            {
                var Tournament = Tournamentservice.Create(TournamentViewModel.Name, TournamentViewModel.TournamentFormat, TournamentViewModel.ClasificationFormat,
                                                          TournamentViewModel.Rules, TournamentViewModel.Prices, TournamentViewModel.Headquarters, TournamentViewModel.Dates, TournamentViewModel.TeamsPlayers);

                return Request.CreateResponse(HttpStatusCode.Created, Tournament.GetId());
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [HttpPost]
        [Route("{tournamentID}/fixture")]
        public HttpResponseMessage GenerateFixture(int tournamentID)
        {
            try
            {
                Tournamentservice.GenerateFixture(tournamentID);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public IEnumerable<Tournament> Get()
        {
            return Tournamentservice.GetAll();
        }

        [Route("{tournamentID}/positions")]
        public IEnumerable<ExpandoObject> GetPositions([FromUri] int tournamentID)
        {
            return Tournamentservice.GetTournamentPositions(tournamentID);
        }

        [HttpPut]
        [Route("{tournamentID}")]
        public HttpResponseMessage Put([FromUri] int tournamentID, [FromBody]PutTournamentViewModel PutTournamentViewModel)
        {
            try
            {
                Tournamentservice.Update(tournamentID, PutTournamentViewModel.Name, PutTournamentViewModel.TournamentFormat, PutTournamentViewModel.ClasificationFormat,
                                                          PutTournamentViewModel.Rules, PutTournamentViewModel.Prices, PutTournamentViewModel.Headquarters, PutTournamentViewModel.Dates, PutTournamentViewModel.TeamsPlayers);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

    }
}