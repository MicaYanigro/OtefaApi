using Otefa.Domain.Model.Entities;
using Otefa.Domain.Model.Exceptions;
using Otefa.Domain.Model.Services;
using Otefa.Infrastructure.IoC;
using Otefa.UI.Api.ViewModel.Player;
using Otefa.UI.Api.ViewModel.Team;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<HttpResponseMessage> Post(MatchViewModel MatchViewModel)
        {
            try
            {
                var Match = await MatchService.Create(MatchViewModel.Tournament, MatchViewModel.Group, MatchViewModel.Headquarter, MatchViewModel.Date, MatchViewModel.Round, MatchViewModel.Teams);

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

        [HttpGet]
        [Route("{matchID}")]
        public async Task<HttpResponseMessage> GetById(int matchID)
        {
            var result =  await MatchService.GetById(matchID);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        [Route("getByTournament/{tournamentID}")]
        public HttpResponseMessage GetByTournamentID(int tournamentID)
        {
            var result =  MatchService.GetByTournamentId(tournamentID);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }



        [HttpPut]
        [Route("{matchID}")]
        public async Task<HttpResponseMessage> Put([FromUri] int matchID, [FromBody]PutMatchViewModel PutMatchViewModel)
        {
            try
            {
               await MatchService.Update(matchID, PutMatchViewModel.Headquarter, PutMatchViewModel.Date);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }
        }

        [HttpPut]
        [Route("results/{matchID}")]
        public HttpResponseMessage LoadResults([FromUri] int matchID, [FromBody] ResultsMatchViewModel ResultsMatchViewModel)
        {
            try
            {

                var playersDetails = ConvertPlayerDetailsViewModelCollectionToDynamicCollection(ResultsMatchViewModel.PlayersDetails);
                MatchService.LoadResults(matchID, ResultsMatchViewModel.MatchTeamID, ResultsMatchViewModel.Goals, ResultsMatchViewModel.AgainstGoals, ResultsMatchViewModel.HasBonusPoint, ResultsMatchViewModel.FigureID, playersDetails);


                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ExceptionBase e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.Message));
            }

      
        }

        public IEnumerable<ExpandoObject> ConvertPlayerDetailsViewModelCollectionToDynamicCollection(IEnumerable<PlayersDetailsViewModel> playersDetails)
        {
            var items = new List<ExpandoObject>();
            foreach (var playerDetails in playersDetails)
            {
                dynamic item = new ExpandoObject();

                item.PlayerID = playerDetails.PlayerID;
                item.Goals = playerDetails.Goals;
                item.Played = playerDetails.Played;
                item.Card = playerDetails.Card;
                item.Observation = playerDetails.Observation;
             
                items.Add(item);
            }
            return items;
        }

    }
}