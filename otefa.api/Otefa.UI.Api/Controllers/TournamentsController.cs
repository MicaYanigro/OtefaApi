using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<Tournament> GetByID(int id)
        {
            return await Tournamentservice.GetByID(id);
        }

        [HttpGet]
        [Route("Get/{name}")]
        public Tournament GetByName(string name)
        {
            return Tournamentservice.FindTournamentByName(name);
        }

        [HttpGet]
        [Route("{tournamentID}/matches")]
        public async Task<HttpResponseMessage> GetMatches(int tournamentID)
        {
           var response = await Tournamentservice.GetAllMatchesByTournament(tournamentID);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> Post(TournamentViewModel TournamentViewModel)
        {
            try
            {
                var Tournament = await Tournamentservice.Create(TournamentViewModel.Name, TournamentViewModel.TournamentFormat, TournamentViewModel.ClasificationFormat,
                                                          TournamentViewModel.Rules, TournamentViewModel.Prices, TournamentViewModel.Headquarters, TournamentViewModel.Dates, TournamentViewModel.TeamsPlayers);

                var tournamentTeamPlayers = Tournament.GetTeamPlayers();
                var response = new { id = Tournament.GetId(), teams = tournamentTeamPlayers };

                return Request.CreateResponse(HttpStatusCode.Created, response);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [HttpPost]
        [Route("{tournamentID}/groups")]
        public async Task<HttpResponseMessage> PostGroups([FromUri] int tournamentID, [FromBody] List<GroupsViewModel> GroupsViewModel)
        {
            try
            {
                foreach (var Group in GroupsViewModel)
                {
                    await Tournamentservice.AddGroups(tournamentID, Group.Name, Group.TeamsID);
                }

                return Request.CreateResponse(HttpStatusCode.Created);
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

        [HttpPost]
        [Route("{tournamentID}/{groupID}/fixture")]
        public HttpResponseMessage GenerateFixtureByGroup([FromUri] int tournamentID, [FromUri] int groupID)
        {
            try
            {
                Tournamentservice.GenerateFixtureByGroup(tournamentID, groupID);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [Route("")]
        public HttpResponseMessage Get()
        {
            var result = Tournamentservice.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("{tournamentID}/positions")]
        public HttpResponseMessage GetPositions([FromUri] int tournamentID)
        {
            var result = Tournamentservice.GetTournamentPositions(tournamentID);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("{tournamentID}/positionsByGroups")]
        public HttpResponseMessage GetPositionsByGroups([FromUri] int tournamentID)
        {
            var result = Tournamentservice.GetTournamentPositionsByGroups(tournamentID);
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{tournamentID}")]
        public async Task<HttpResponseMessage> Put([FromUri] int tournamentID, [FromBody]PutTournamentViewModel PutTournamentViewModel)
        {
            try
            {
                await Tournamentservice.Update(tournamentID, PutTournamentViewModel.Name, PutTournamentViewModel.TournamentFormat, PutTournamentViewModel.ClasificationFormat,
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